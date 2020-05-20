using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Disease;
using Biod.Products.Common;
using Biod.Products.Common.Constants;
using Biod.Products.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class DiseaseService : IDiseaseService
    {
        private readonly BiodZebraContext _biodZebraContext;
        private readonly ILogger<DiseaseService> _logger;
        private readonly IGeonameService _geonameService;
        private readonly ICaseCountService _caseCountService;
        
        public DiseaseService(BiodZebraContext biodZebraContext, ILogger<DiseaseService> logger, IGeonameService geonameService, ICaseCountService caseCountService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _geonameService = geonameService;
            _caseCountService = caseCountService;
        }

        public async Task<IEnumerable<DiseaseInformationModel>> GetDiseases(DiseaseConfig diseaseConfig)
        {
            var diseases = (await new DiseaseQueryBuilder(_biodZebraContext, diseaseConfig).BuildAndExecute()).ToList();
            return diseases.Select(d => ConvertToModel(d, diseaseConfig));
        }

        public async Task<CaseCountModel> GetDiseaseCaseCount(int diseaseId, int? geonameId)
        {
            return await GetDiseaseCaseCount(diseaseId, geonameId, null);
        }

        public async Task<CaseCountModel> GetDiseaseCaseCount(int diseaseId, int? geonameId, int? eventId)
        {
            if (!geonameId.HasValue) // Global case count
            {
                var globalCaseCount = (await SqlQuery.GetLocalCaseCount(_biodZebraContext, diseaseId, null, eventId)).First();

                return new CaseCountModel
                {
                    ReportedCases = globalCaseCount.CaseCount
                };
            }
            
            var geoname = await _geonameService.GetGeoname(new GeonameConfig.Builder().AddGeonameId(geonameId.Value).Build());

            // Event ID is not passed since we want all events with proximal locations to be included
            var result = (await _caseCountService.GetProximalCaseCount(geoname, diseaseId, null)).ToList();

            return new CaseCountModel
            {
                ReportedCases = result.Sum(x => x.ProximalCases)
            };
        }

        public async Task<DiseaseInformationModel> GetDisease(DiseaseConfig diseaseConfig)
        {
            var disease = (await new DiseaseQueryBuilder(_biodZebraContext, diseaseConfig).BuildAndExecute()).FirstOrDefault();

            if (disease == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested disease with id {diseaseConfig.GetDiseaseId()} does not exist");
            }

            return ConvertToModel(disease, diseaseConfig);
        }

        public async Task<IEnumerable<DiseaseGroupModel>> GetDiseaseGroups()
        {
            var diseases = (await new DiseaseQueryBuilder(_biodZebraContext, new DiseaseConfig.Builder()
                        .ShouldIncludeAcquisitionModes()
                        .Build())
                    .BuildAndExecute())
                .ToList();

            var result = new List<DiseaseGroupModel>
            {
                GroupDiseaseByModesOfAcquisition(diseases),
                GroupDiseaseByAlphabetical(diseases)
            };

            return result;
        }

        private DiseaseInformationModel ConvertToModel(DiseaseJoinResult result, DiseaseConfig diseaseConfig)
        {
            var agentsText = diseaseConfig.IncludeAgents ? string.Join(", ", result.Agents).DefaultIfWhiteSpace() : null;
            var agentTypesText = diseaseConfig.IncludeAgents ? string.Join(", ", result.AgentTypes).DefaultIfWhiteSpace() : null;

            var preventionMeasureText = diseaseConfig.IncludeInterventions ? string.Join(", ", result.PreventionMeasures).DefaultIfWhiteSpace(InterventionType.BehaviouralOnly) : null;

            var transmissionModesText = diseaseConfig.IncludeTransmissionModes ? string.Join(", ", result.TransmissionModes) : null;

            var incubationPeriodText = diseaseConfig.IncludeIncubationPeriods
                ? StringFormattingHelper.FormatPeriod(
                    result.IncubationPeriod?.IncubationMinimumSeconds,
                    result.IncubationPeriod?.IncubationMaximumSeconds,
                    result.IncubationPeriod?.IncubationAverageSeconds)
                : null;

            var symptomaticPeriodText = diseaseConfig.IncludeSymptomaticPeriods
                ? StringFormattingHelper.FormatPeriod(
                    result.SymptomaticPeriod?.SymptomaticMinimumSeconds,
                    result.SymptomaticPeriod?.SymptomaticMaximumSeconds,
                    result.SymptomaticPeriod?.SymptomaticAverageSeconds)
                : null;

            var acquisitionModes = diseaseConfig.IncludeAcquisitionModes
                ? result.AcquisitionModes?
                    .GroupBy(a => a.RankId)
                    .OrderBy(a => a.Key)
                    .Select(g => new AcquisitionModeGroupModel
                    {
                        RankId = g.Key,
                        RankName = ((AcquisitionModeRankType) g.Key).ToString(),
                        AcquisitionModes = g.OrderBy(a => a.Label)
                    }) ?? new List<AcquisitionModeGroupModel>()
                : null;

            var biosecurityRiskText = diseaseConfig.IncludeBiosecurityRisks ? result.BiosecurityRisk.DefaultIfWhiteSpace() : null;

            return new DiseaseInformationModel
            {
                Id = result.DiseaseId,
                Name = result.DiseaseName,
                Agents = agentsText,
                AgentTypes = agentTypesText,
                AcquisitionModeGroups = acquisitionModes,
                PreventionMeasure = preventionMeasureText,
                TransmissionModes = transmissionModesText,
                IncubationPeriod = incubationPeriodText,
                SymptomaticPeriod = symptomaticPeriodText,
                BiosecurityRisk = biosecurityRiskText
            };
        }

        private DiseaseGroupModel GroupDiseaseByModesOfAcquisition(List<DiseaseJoinResult> diseases)
        {
            var groupedByAcquisitionModes = new DiseaseGroupModel
            {
                GroupId = 1,
                GroupName = "Mode of Acquisition",
                SubGroups = diseases
                    .SelectMany(d => d.AcquisitionModes
                        .Where(a => a.RankId == (int) AcquisitionModeRankType.Common || a.RankId == (int) AcquisitionModeRankType.Uncommon)
                        .Select(a => new {AcquisitionMode = a, d.DiseaseId, d.DiseaseName}))
                    .GroupBy(a => new {a.AcquisitionMode.VectorId, a.AcquisitionMode.VectorName})
                    .Select(vg => new DiseaseGroupModel
                    {
                        GroupId = vg.Key.VectorId,
                        GroupName = vg.Key.VectorName,
                        SubGroups = vg
                            .GroupBy(a => new {a.AcquisitionMode.ModalityId, a.AcquisitionMode.ModalityName})
                            .Select(mg => new DiseaseGroupModel
                            {
                                GroupId = mg.Key.ModalityId,
                                GroupName = mg.Key.ModalityName,
                                DiseaseIds = mg.OrderBy(a => a.DiseaseName).Select(a => a.DiseaseId).ToList()
                            })
                            .OrderBy(sg => sg.GroupName)
                            .ToList()
                    })
                    .OrderBy(sg => sg.GroupName)
                    .ToList()
            };
            var groupedDiseaseIds = new HashSet<int>(groupedByAcquisitionModes.SubGroups.SelectMany(sg => sg.SubGroups.SelectMany(ssg => ssg.DiseaseIds)));
            var ungroupedDiseaseIds = new HashSet<int>(diseases.Select(d => d.DiseaseId)).Except(groupedDiseaseIds);
            groupedByAcquisitionModes.SubGroups.Add(new DiseaseGroupModel
            {
                GroupId = -1,
                GroupName = "Ungrouped Diseases",
                DiseaseIds = diseases
                    .Where(d => ungroupedDiseaseIds.Contains(d.DiseaseId))
                    .OrderBy(d => d.DiseaseName)
                    .Select(d => d.DiseaseId)
                    .ToList()
            });

            return groupedByAcquisitionModes;
        }

        private DiseaseGroupModel GroupDiseaseByAlphabetical(List<DiseaseJoinResult> diseases)
        {
            return new DiseaseGroupModel
            {
                GroupId = 2,
                GroupName = "Alphabetical",
                SubGroups = diseases.GroupBy(
                        d => d.DiseaseName.Substring(0, 1).ToUpper(),
                        (letter, subList) => new
                        {
                            Letter = letter,
                            DiseaseIds = subList.OrderBy(d => d.DiseaseName).Select(d => d.DiseaseId).ToList()
                        })
                    .OrderBy(x => x.Letter)
                    .Select((x, i) => new DiseaseGroupModel
                    {
                        GroupId = i,
                        GroupName = x.Letter,
                        DiseaseIds = x.DiseaseIds
                    })
                    .ToList()
            };
        }
    }
}