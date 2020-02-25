using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Disease;
using Biod.Insights.Api.Models.Geoname;
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
        private readonly ICaseCountService _caseCountService;

        /// <summary>
        /// Risk service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseService">The disease service</param>
        /// <param name="outbreakPotentialService">The outbreak potential service</param>
        /// <param name="geonameService">The geoname service</param>
        /// <param name="mapService">The map service</param>
        /// <param name="caseCountService">The case count service</param>
        public DiseaseRiskService(
            BiodZebraContext biodZebraContext,
            ILogger<DiseaseRiskService> logger,
            IDiseaseService diseaseService,
            IOutbreakPotentialService outbreakPotentialService,
            IGeonameService geonameService,
            IMapService mapService,
            ICaseCountService caseCountService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseService = diseaseService;
            _outbreakPotentialService = outbreakPotentialService;
            _geonameService = geonameService;
            _mapService = mapService;
            _caseCountService = caseCountService;
        }

        public async Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId)
        {
            return await GetDiseaseRiskForLocation(geonameId, new int[0]);
        }

        public async Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId, DiseaseRelevanceSettingsModel relevanceSettings)
        {
            var result = await GetDiseaseRiskForLocation(geonameId, relevanceSettings.GetRelevantDiseases());
            result.DiseaseRisks = DiseaseRelevanceHelper.FilterRelevantDiseases(result.DiseaseRisks, relevanceSettings);
            var shownDiseaseIds = new HashSet<int>(result.DiseaseRisks.Select(r => r.DiseaseInformation.Id));
            result.CountryPins = result.CountryPins
                .Select(p =>
                {
                    p.Events = p.Events.Where(e => shownDiseaseIds.Contains(e.DiseaseId)).ToList();
                    return p;
                })
                .Where(p => p.Events.Any());

            return result;
        }

        public async Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId, [NotNull] IEnumerable<int> diseaseIds)
        {
            var eventQueryBuilder = new EventQueryBuilder(_biodZebraContext)
                .AddDiseaseIds(diseaseIds)
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
                        
                        // Calculate aggregated case counts only when no locations (local case counts not applicable)
                        var eventCaseCounts = geoname == null
                            ? g.Select(e =>
                            {
                                var caseCounts = _caseCountService.GetCaseCountTree(e.XtblEventLocations.ToList());
                                return new CaseCountModel
                                {
                                    ReportedCases = caseCounts.Sum(c => c.Value.GetNestedRepCaseCount()),
                                    ConfirmedCases = caseCounts.Sum(c => c.Value.GetNestedConfCaseCount()),
                                    SuspectedCases = caseCounts.Sum(c => c.Value.GetNestedSuspCaseCount()),
                                    Deaths = caseCounts.Sum(c => c.Value.GetNestedDeathCount()),
                                    HasReportedCasesNesting = caseCounts.Any(c => c.Value.HasRepCaseNestingApplied),
                                    HasConfirmedCasesNesting = caseCounts.Any(c => c.Value.HasConfCaseNestingApplied),
                                    HasSuspectedCasesNesting = caseCounts.Any(c => c.Value.HasSuspCaseNestingApplied),
                                    HasDeathsNesting = caseCounts.Any(c => c.Value.HasDeathNestingApplied)
                                };
                            }).ToList() : null;
                        
                        return new DiseaseRiskModel
                        {
                            DiseaseInformation = disease,
                            ImportationRisk = geoname != null ? RiskCalculationHelper.CalculateImportationRisk(g.ToList()) : null,
                            ExportationRisk = RiskCalculationHelper.CalculateExportationRisk(g.ToList()),
                            LastUpdatedEventDate = g.OrderByDescending(e => e.Event.LastUpdatedDate).First().Event.LastUpdatedDate,
                            OutbreakPotentialCategory = outbreakPotentialCategories.FirstOrDefault(o => o.DiseaseId == g.Key),
                            HasLocalEvents = g.Any(e => e.ImportationRisk?.LocalSpread == 1),
                            CaseCounts = eventCaseCounts != null ? new CaseCountModel
                            {
                                ReportedCases = eventCaseCounts.Sum(e => e.ReportedCases),
                                ConfirmedCases = eventCaseCounts.Sum(e => e.ConfirmedCases),
                                SuspectedCases = eventCaseCounts.Sum(e => e.SuspectedCases),
                                Deaths = eventCaseCounts.Sum(e => e.Deaths),
                                HasReportedCasesNesting = eventCaseCounts.Any(e => e.HasReportedCasesNesting ?? false),
                                HasConfirmedCasesNesting = eventCaseCounts.Any(e => e.HasConfirmedCasesNesting ?? false),
                                HasSuspectedCasesNesting = eventCaseCounts.Any(e => e.HasSuspectedCasesNesting ?? false),
                                HasDeathsNesting = eventCaseCounts.Any(e => e.HasDeathsNesting ?? false)
                            } : null
                        };
                    }),
                CountryPins = await _mapService.GetCountryEventPins(new HashSet<int>(events.Select(e => e.Event.EventId)))
            };
        }
    }
}