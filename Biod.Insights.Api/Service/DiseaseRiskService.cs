using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Disease;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class DiseaseRiskService : IDiseaseRiskService
    {
        private readonly ILogger<DiseaseRiskService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDiseaseService _diseaseService;

        /// <summary>
        /// Risk service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseService">The disease service</param>
        public DiseaseRiskService(
            BiodZebraContext biodZebraContext,
            ILogger<DiseaseRiskService> logger,
            IDiseaseService diseaseService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseService = diseaseService;
        }
        
        public async Task<IEnumerable<GetDiseaseRiskModel>> GetDiseaseRiskForLocation(int geonameId)
        {
            var events = await _biodZebraContext.usp_ZebraEventGetEventSummary_Result
                .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventGetEventSummary
                                            @UserId = {""},
		                                    @GeonameIds = {geonameId},
		                                    @DiseasesIds = {""},
		                                    @TransmissionModesIds = {""},
		                                    @InterventionMethods = {""},
		                                    @SeverityRisks = {""},
		                                    @BiosecurityRisks = {""},
		                                    @LocationOnly = {0}")
                .ToListAsync();

            var diseases = (await _diseaseService.GetDiseases()).ToList();

            return events
                .GroupBy(e => e.DiseaseId)
                .Select(g =>
                {
                    var disease = diseases.First(d => d.Id == g.Key);
                    return new GetDiseaseRiskModel
                    {
                        DiseaseInformation = disease,
                        ImportationRisk = RiskCalculationHelper.CalculateImportationRiskCompat(g.ToList()),
                        LastUpdatedEventDate = g.OrderByDescending(e => e.LastUpdatedDate).First().LastUpdatedDate
                    };
                });
        }
    }
}