using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
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
        private readonly IMapService _mapService;

        /// <summary>
        /// Risk service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseService">The disease service</param>
        /// <param name="outbreakPotentialService">The outbreak potential service</param>
        /// <param name="geonameService">The geoname service</param>
        /// <param name="mapService">The map service</param>
        public DiseaseRiskService(
            BiodZebraContext biodZebraContext,
            ILogger<DiseaseRiskService> logger,
            IDiseaseService diseaseService,
            IOutbreakPotentialService outbreakPotentialService,
            IGeonameService geonameService,
            IMapService mapService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseService = diseaseService;
            _outbreakPotentialService = outbreakPotentialService;
            _geonameService = geonameService;
            _mapService = mapService;
        }

        public async Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId)
        {
            var eventQueryBuilder = new EventQueryBuilder(_biodZebraContext)
                .IncludeExportationRisk()
                .IncludeLocations();
            
            GetGeonameModel geoname = null;
            if (geonameId.HasValue)
            {
                // Importation risk required
                geoname = await _geonameService.GetGeoname(geonameId.Value);
                eventQueryBuilder.IncludeImportationRisk(geonameId.Value);
            }

            var events = (await eventQueryBuilder.BuildAndExecute()).ToList();
            var diseases = (await _diseaseService.GetDiseases()).ToList();
            
            var outbreakPotentialCategories = new List<OutbreakPotentialCategoryModel>();
            if (geoname != null)
            {
                outbreakPotentialCategories = (await _outbreakPotentialService.GetOutbreakPotentialByGeoname(geoname)).ToList();
            }

            return new RiskAggregationModel
            {
                DiseaseRisks = events
                    .GroupBy(e => e.Event.DiseaseId)
                    .Select(g =>
                    {
                        var disease = diseases.First(d => d.Id == g.Key);
                        return new DiseaseRiskModel
                        {
                            DiseaseInformation = disease,
                            ImportationRisk = geoname != null ? RiskCalculationHelper.CalculateImportationRisk(g.ToList()) : null,
                            ExportationRisk = RiskCalculationHelper.CalculateExportationRisk(g.ToList()),
                            LastUpdatedEventDate = g.OrderByDescending(e => e.Event.LastUpdatedDate).First().Event.LastUpdatedDate.Value, // Last updated date can never be null
                            OutbreakPotentialCategory = outbreakPotentialCategories.FirstOrDefault(o => o.DiseaseId == g.Key)
                        };
                    }),
                CountryPins = await _mapService.GetCountryEventPins(new HashSet<int>(events.Select(e => e.Event.EventId)))
            };
        }
    }
}