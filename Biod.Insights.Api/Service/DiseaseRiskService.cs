using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Disease;
using Biod.Insights.Api.Models.Geoname;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class DiseaseRiskService : IDiseaseRiskService
    {
        private readonly ILogger<DiseaseRiskService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDiseaseService _diseaseService;
        private readonly IOutbreakPotentialService _outbreakPotentialService;
        private readonly IGeonameService _geonameService;

        /// <summary>
        /// Risk service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseService">The disease service</param>
        /// <param name="outbreakPotentialService">The outbreak potential service</param>
        /// <param name="geonameService">The geoname service</param>
        public DiseaseRiskService(
            BiodZebraContext biodZebraContext,
            ILogger<DiseaseRiskService> logger,
            IDiseaseService diseaseService,
            IOutbreakPotentialService outbreakPotentialService,
            IGeonameService geonameService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseService = diseaseService;
            _outbreakPotentialService = outbreakPotentialService;
            _geonameService = geonameService;
        }

        public async Task<IEnumerable<GetDiseaseRiskModel>> GetDiseaseRiskForLocation(int? geonameId)
        {
            GetGeonameModel geoname = null;
            if (geonameId.HasValue)
            {
                geoname = await _geonameService.GetGeoname(geonameId.Value);
            }

            var events = await _biodZebraContext.usp_ZebraEventGetEventSummary_Result
                .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventGetEventSummary
                                            @UserId = {""},
		                                    @GeonameIds = {(geoname != null ? geoname.GeonameId.ToString() : "")},
		                                    @DiseasesIds = {""},
		                                    @TransmissionModesIds = {""},
		                                    @InterventionMethods = {""},
		                                    @SeverityRisks = {""},
		                                    @BiosecurityRisks = {""},
		                                    @LocationOnly = {0}")
                .ToListAsync();

            var diseases = (await _diseaseService.GetDiseases()).ToList();
            var outbreakPotentialCategories = new List<OutbreakPotentialCategoryModel>();
            if (geoname != null)
            {
                outbreakPotentialCategories = (await _outbreakPotentialService.GetOutbreakPotentialByGeoname(geoname)).ToList();
            }

            return events
                .GroupBy(e => e.DiseaseId)
                .Select(g =>
                {
                    var disease = diseases.First(d => d.Id == g.Key);
                    return new GetDiseaseRiskModel
                    {
                        DiseaseInformation = disease,
                        ImportationRisk = geoname != null ? RiskCalculationHelper.CalculateImportationRiskCompat(g.ToList()) : null,
                        ExportationRisk = RiskCalculationHelper.CalculateExportationRiskCompat(g.ToList()),
                        LastUpdatedEventDate = g.OrderByDescending(e => e.LastUpdatedDate).First().LastUpdatedDate,
                        OutbreakPotentialCategory = outbreakPotentialCategories.FirstOrDefault(o => o.DiseaseId == g.Key)
                    };
                });
        }
    }
}