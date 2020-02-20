using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Constants;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Disease;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
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

        public async Task<IEnumerable<DiseaseInformationModel>> GetDiseases()
        {
            var diseases = (await new DiseaseQueryBuilder(_biodZebraContext)
                    .IncludeAll()
                    .BuildAndExecute())
                .ToList();
            return diseases.Select(ConvertToModel);
        }

        public async Task<CaseCountModel> GetDiseaseCaseCount(int diseaseId, int? geonameId)
        {
            return await GetDiseaseCaseCount(diseaseId, geonameId, null);
        }
        
        public async Task<CaseCountModel> GetDiseaseCaseCount(int diseaseId, int? geonameId, int? eventId)
        {
            var result = (await _biodZebraContext.usp_ZebraDiseaseGeLocalCaseCount_Result
                    .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraDiseaseGetLocalCaseCount
                                            @DiseaseId = {diseaseId},
                                            @GeonameIds = {(geonameId.HasValue ? geonameId.Value.ToString() : "")},
                                            @EventId = {eventId}")
                    .ToListAsync())
                .First();

            return new CaseCountModel
            {
                ReportedCases = result.CaseCount
            };
        }

        public async Task<DiseaseInformationModel> GetDisease(int diseaseId)
        {
            var disease = (await new DiseaseQueryBuilder(_biodZebraContext)
                    .AddDiseaseId(diseaseId)
                    .IncludeAll()
                    .BuildAndExecute())
                .FirstOrDefault();

            if (disease == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested disease with id {diseaseId} does not exist");
            }

            return ConvertToModel(disease);
        }

        private DiseaseInformationModel ConvertToModel(DiseaseJoinResult result)
        {
            var agentsText = string.Join(", ", result.Agents);
            var agentTypesText = string.Join(", ", result.AgentTypes);
            var preventionMeasureText = string.Join(", ", result.PreventionMeasures);
            var transmissionModesText = string.Join(", ", result.TransmissionModes);
            var incubationPeriodText = StringFormattingHelper.FormatIncubationPeriod(
                result.IncubationPeriod?.IncubationMinimumSeconds,
                result.IncubationPeriod?.IncubationMaximumSeconds,
                result.IncubationPeriod?.IncubationAverageSeconds);
            var acquisitionModes = result.AcquisitionModes?
                .GroupBy(a => a.RankId)
                .OrderBy(a => a.Key)
                .Select(g => new AcquisitionModeGroupModel
                {
                    RankId = g.Key,
                    RankName = ((AcquisitionModeRankType) g.Key).ToString(),
                    AcquisitionModes = g.OrderBy(a => a.Label)
                });

            return new DiseaseInformationModel
            {
                Id = result.DiseaseId,
                Name = result.DiseaseName,
                Agents = !string.IsNullOrWhiteSpace(agentsText) ? agentsText : "—",
                AgentTypes = !string.IsNullOrWhiteSpace(agentTypesText) ? agentTypesText : "—",
                AcquisitionModeGroups = acquisitionModes ?? new List<AcquisitionModeGroupModel>(),
                PreventionMeasure = !string.IsNullOrWhiteSpace(preventionMeasureText) ? preventionMeasureText : InterventionType.BehaviouralOnly,
                TransmissionModes = !string.IsNullOrWhiteSpace(transmissionModesText) ? transmissionModesText : "—",
                IncubationPeriod = !string.IsNullOrWhiteSpace(incubationPeriodText) ? incubationPeriodText : "—",
                BiosecurityRisk = !string.IsNullOrWhiteSpace(result.BiosecurityRisk) ? result.BiosecurityRisk : "—"
            };
        }
    }
}