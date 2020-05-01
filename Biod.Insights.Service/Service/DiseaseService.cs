using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Products.Common;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Disease;
using Biod.Products.Common.Constants;
using Biod.Products.Common.Exceptions;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class DiseaseService : IDiseaseService
    {
        private readonly ILogger<DiseaseService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// Disease service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public DiseaseService(BiodZebraContext biodZebraContext, ILogger<DiseaseService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
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
            var result = (await SqlQuery.GetLocalCaseCount(_biodZebraContext, diseaseId, geonameId, eventId)).First();

            return new CaseCountModel
            {
                ReportedCases = result.CaseCount
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
    }
}