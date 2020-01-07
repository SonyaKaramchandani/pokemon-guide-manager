using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            var result = (await _biodZebraContext.usp_ZebraDiseaseGeLocalCaseCount_Result
                    .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraDiseaseGetLocalCaseCount
                                            @DiseaseId = {diseaseId},
                                            @GeonameIds = {(geonameId.HasValue ? geonameId.Value.ToString() : "")}")
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
                    .SetDiseaseId(diseaseId)
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
            var agentsText = string.Join(", ", result.Disease.XtblDiseaseAgents
                .Select(x => x.Agent.Agent)
                .OrderBy(a => a));

            var agentTypesText = string.Join(", ", result.Disease.XtblDiseaseAgents
                .Select(x => x.Agent.AgentType.AgentType)
                .Distinct()
                .OrderBy(a => a));

            var preventionMeasureText = string.Join(", ", result.Disease.XtblDiseaseInterventions
                .Where(x => x.SpeciesId == (int) Constants.Species.Human)
                .Select(x => x.Intervention)
                .Where(i => i.InterventionType == Constants.InterventionType.Prevention.ToString())
                .Select(i => i.DisplayName)
                .Distinct()
                .OrderBy(a => a));

            var transmissionModesText = string.Join(", ", result.Disease.XtblDiseaseTransmissionMode
                .Where(x => x.SpeciesId == (int) Constants.Species.Human)
                .Select(x => x.TransmissionMode.DisplayName)
                .Distinct()
                .OrderBy(a => a));

            var incubationPeriodText = result.Disease.DiseaseSpeciesIncubation
                                           .Where(i => i.SpeciesId == (int) Constants.Species.Human)
                                           .Select(i => StringFormattingHelper.FormatIncubationPeriod(i.IncubationMinimumSeconds, i.IncubationMaximumSeconds, i.IncubationAverageSeconds))
                                           .FirstOrDefault() ?? "-";

            return new DiseaseInformationModel
            {
                Id = result.Disease.DiseaseId,
                Name = result.Disease.DiseaseName,
                Agents = !string.IsNullOrWhiteSpace(agentsText) ? agentsText : "-",
                AgentTypes = !string.IsNullOrWhiteSpace(agentTypesText) ? agentTypesText : "-",
                PreventionMeasure = !string.IsNullOrWhiteSpace(preventionMeasureText) ? preventionMeasureText : "-",
                TransmissionModes = !string.IsNullOrWhiteSpace(transmissionModesText) ? transmissionModesText : "-",
                IncubationPeriod = incubationPeriodText,
                BiosecurityRisk = result.BiosecurityRisk?.BiosecurityRiskDesc ?? "-"
            };
        }
    }
}