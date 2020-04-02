using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Service.Models.Article;
using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Service.Models.Event;
using Biod.Insights.Service.Models.Geoname;
using Biod.Insights.Common.Constants;
using Biod.Insights.Common.Exceptions;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data;
using Microsoft.Extensions.Logging;
using Biod.Insights.Service.Models.Map;

namespace Biod.Insights.Service.Service
{
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDiseaseService _diseaseService;
        private readonly IGeonameService _geonameService;
        private readonly IOutbreakPotentialService _outbreakPotentialService;
        private readonly IAirportService _airportService;
        private readonly IMapService _mapService;
        private readonly ICaseCountService _caseCountService;

        /// <summary>
        /// Event service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseService">the disease service</param>
        /// <param name="geonameService">the geoname service</param>
        /// <param name="outbreakPotentialService">the outbreak potential service</param>
        /// <param name="airportService">the airport service</param>
        /// <param name="mapService">the map service</param>
        /// <param name="caseCountService">the case count service</param>
        public EventService(
            BiodZebraContext biodZebraContext,
            ILogger<EventService> logger,
            IDiseaseService diseaseService,
            IGeonameService geonameService,
            IOutbreakPotentialService outbreakPotentialService,
            IAirportService airportService,
            IMapService mapService,
            ICaseCountService caseCountService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseService = diseaseService;
            _geonameService = geonameService;
            _outbreakPotentialService = outbreakPotentialService;
            _airportService = airportService;
            _mapService = mapService;
            _caseCountService = caseCountService;
        }

        public async Task<EventAirportModel> GetAirports(int eventId, int? geonameId)
        {
            var eventModel = (await new EventQueryBuilder(_biodZebraContext, new EventConfig.Builder()
                        .SetEventId(eventId)
                        .Build())
                    .BuildAndExecute())
                .FirstOrDefault();
            if (eventModel == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested event with id {eventId} does not exist");
            }

            return new EventAirportModel
            {
                SourceAirports = await _airportService.GetSourceAirports(new AirportConfig.Builder(eventId).ShouldIncludeCity().Build()),
                DestinationAirports = await _airportService.GetDestinationAirports(new AirportConfig.Builder(eventId)
                    .ShouldIncludeCity()
                    .ShouldIncludeImportationRisk(geonameId)
                    .Build())
            };
        }

        public async Task<Dictionary<string, Dictionary<int, HashSet<int>>>> GetUsersWithinEventLocations(int eventId)
        {
            var eventModel = await GetEvent(new EventConfig.Builder()
                .SetEventId(eventId)
                .ShouldIncludeLocations()
                .Build());

            var eventLocations = new Dictionary<string, Dictionary<int, HashSet<int>>>();
            foreach (var eventLocation in eventModel.EventLocations)
            {
                var users = await SqlQuery.GetUsersWithinGeoname(_biodZebraContext, eventLocation.GeonameId, eventModel.EventInformation.DiseaseId);

                users.ForEach(u =>
                {
                    var userLocations = eventLocations.ContainsKey(u.UserId) ? eventLocations[u.UserId] : new Dictionary<int, HashSet<int>>();
                    var eventLocationsForAoi = userLocations.ContainsKey(u.UserGeonameId) ? userLocations[u.UserGeonameId] : new HashSet<int>();
                    eventLocationsForAoi.Add(eventLocation.GeonameId);

                    userLocations[u.UserGeonameId] = eventLocationsForAoi;
                    eventLocations[u.UserId] = userLocations;
                });
            }

            return eventLocations;
        }

        public async Task<GetEventListModel> GetEvents(EventConfig eventConfig)
        {
            // Load Geoname if provided
            GetGeonameModel geoname = null;
            if (eventConfig.IncludeImportationRisk && eventConfig.GeonameId.HasValue)
            {
                geoname = await _geonameService.GetGeoname(new GeonameConfig.Builder().AddGeonameId(eventConfig.GeonameId.Value).Build());
            }

            // Get the list of events
            var events = (await new EventQueryBuilder(_biodZebraContext, eventConfig).BuildAndExecute()).ToList();
            var eventModels = new List<GetEventModel>();
            foreach (var e in events)
            {
                eventModels.Add(await ConvertToModel(e, geoname, eventConfig));
            }

            // Begin constructing model to be returned
            var returnedModel = new GetEventListModel
            {
                EventsList = OrderingHelper.OrderEventsByDefault(eventModels),
                CountryPins = await _mapService.GetCountryEventPins(new HashSet<int>(events.Select(e => e.Event.EventId)))
            };

            if (eventConfig.DiseaseIds.Count == 1)
            {
                // Only single disease was provided, properties that can be aggregated over the disease can be loaded (e.g. aggregating Measles risk as a whole)
                var disease = await _diseaseService.GetDisease(new DiseaseConfig.Builder()
                    .ShouldIncludeAllProperties()
                    .AddDiseaseId(eventConfig.DiseaseIds.First())
                    .Build());

                returnedModel.DiseaseInformation = disease;
                returnedModel.ImportationRisk = eventConfig.IncludeImportationRisk ? RiskCalculationHelper.CalculateImportationRisk(events) : null;
                returnedModel.ExportationRisk = eventConfig.IncludeExportationRisk ? RiskCalculationHelper.CalculateExportationRisk(events) : null;
                returnedModel.OutbreakPotentialCategory = eventConfig.IncludeOutbreakPotential ? await _outbreakPotentialService.GetOutbreakPotentialByGeoname(disease.Id, geoname) : null;
                returnedModel.LocalCaseCounts = geoname != null ? await _diseaseService.GetDiseaseCaseCount(disease.Id, geoname.GeonameId) : null;
            }

            return returnedModel;
        }

        public async Task<GetEventListModel> GetEvents(EventConfig eventConfig, DiseaseRelevanceSettingsModel relevanceSettings)
        {
            var diseaseIds = relevanceSettings.GetRelevantDiseases();
            if (eventConfig.DiseaseIds.Count == 1)
            {
                // Check whether disease exists
                var disease = await _diseaseService.GetDisease(new DiseaseConfig.Builder().AddDiseaseId(eventConfig.DiseaseIds.First()).Build());
                diseaseIds.IntersectWith(new[] {disease.Id});
            }

            if (!diseaseIds.Any())
            {
                // No events since there are no diseases of interest after filtering out preferences
                return new GetEventListModel
                {
                    EventsList = new GetEventModel[0],
                    CountryPins = new EventsPinModel[0]
                };
            }

            return await GetEvents(eventConfig);
        }

        public async Task UpdateEventActivityHistory(int eventId)
        {
            var result = await SqlQuery.UpdateEventLocationHistory(_biodZebraContext, eventId);
            _logger.LogInformation($"Ran event location history update for event {eventId}: Received result: {result}");
        }

        public async Task<GetEventModel> GetEvent(EventConfig eventConfig)
        {
            GetGeonameModel geoname = null;
            if (eventConfig.GeonameId.HasValue)
            {
                geoname = await _geonameService.GetGeoname(new GeonameConfig.Builder().AddGeonameId(eventConfig.GeonameId.Value).Build());
            }

            var @event = (await new EventQueryBuilder(_biodZebraContext, eventConfig).BuildAndExecute()).FirstOrDefault();
            if (@event == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested event with id {eventConfig.EventId} does not exist");
            }

            return await ConvertToModel(@event, geoname, eventConfig);
        }

        private async Task<GetEventModel> ConvertToModel(EventJoinResult result, [AllowNull] GetGeonameModel geoname, EventConfig eventConfig)
        {
            // Begin constructing model to be returned
            var returnedModel = new GetEventModel
            {
                EventInformation = new EventInformationModel
                {
                    Id = result.Event.EventId,
                    Summary = result.Event.Summary,
                    Title = result.Event.EventTitle,
                    StartDate = result.Event.StartDate,
                    EndDate = result.Event.EndDate,
                    LastUpdatedDate = result.Event.LastUpdatedDate,
                    DiseaseId = result.Event.DiseaseId
                },
                ExportationRisk = eventConfig.IncludeExportationRisk ? LoadExportationRisk(result) : null,
                ImportationRisk = eventConfig.IncludeImportationRisk ? LoadImportationRisk(result) : null,
                DiseaseInformation = eventConfig.IncludeDiseaseInformation ? await LoadDiseaseInformation(result.Event.DiseaseId) : null,
                Articles = eventConfig.IncludeArticles ? LoadArticles(result) : null,
                SourceAirports = eventConfig.IncludeSourceAirports ? await LoadSourceAirports(result, eventConfig) : null,
                DestinationAirports = eventConfig.IncludeDestinationAirports ? await LoadDestinationAirports(result, eventConfig) : null,
                OutbreakPotentialCategory = eventConfig.IncludeOutbreakPotential && geoname != null ? await LoadOutbreakPotential(result.Event.DiseaseId, geoname.GeonameId) : null,
                CalculationMetadata = eventConfig.IncludeCalculationMetadata ? LoadCalculationMetadata(result.Event.EventSourceGridSpreadMd.ToList()) : null
            };

            if (eventConfig.IncludeLocations)
            {
                var eventLocations = result.XtblEventLocations.ToList();
                var caseCounts = _caseCountService.GetCaseCountTree(eventLocations);
                var caseCountsFlattened = EventCaseCountModel.FlattenTree(caseCounts);

                returnedModel.CaseCounts = LoadCaseCounts(caseCounts);
                returnedModel.EventLocations = LoadEventLocations(eventLocations, caseCountsFlattened);

                if (eventConfig.IncludeLocalCaseCount && geoname != null)
                {
                    returnedModel.LocalCaseCounts = await _diseaseService.GetDiseaseCaseCount(result.Event.DiseaseId, geoname.GeonameId, result.Event.EventId);
                    returnedModel.IsLocal = returnedModel.LocalCaseCounts?.ReportedCases > 0;
                }

                if (eventConfig.IncludeLocationsHistory)
                {
                    var eventLocationsHistory = result.XtblEventLocationsHistory.ToList();
                    var caseCountsHistory = _caseCountService.GetCaseCountTree(eventLocationsHistory);
                    var caseCountsHistoryFlattened = EventCaseCountModel.FlattenTree(caseCountsHistory);

                    // Increased case counts
                    var deltaCaseCounts = _caseCountService.GetLocationIncreasedCaseCount(caseCountsHistoryFlattened, caseCountsFlattened)
                        .Where(c => c.Value.RawRepCaseCount > 0)
                        .ToDictionary(c => c.Key, c => c.Value);

                    returnedModel.UpdatedLocations = LoadEventLocationsHistory(deltaCaseCounts, eventLocations, eventLocationsHistory);
                    returnedModel.PreviousActivityDate = returnedModel.UpdatedLocations
                        .Where(el => el.PreviousEventDate != null)
                        .Select(el => el.PreviousEventDate)
                        .OrderBy(d => d)
                        .DefaultIfEmpty(result.Event.StartDate)
                        .First();
                }
            }

            return returnedModel;
        }

        private async Task<DiseaseInformationModel> LoadDiseaseInformation(int diseaseId)
        {
            return await _diseaseService.GetDisease(new DiseaseConfig.Builder()
                .ShouldIncludeAllProperties()
                .AddDiseaseId(diseaseId)
                .Build());
        }

        private IEnumerable<ArticleModel> LoadArticles(EventJoinResult eventJoinResult)
        {
            return eventJoinResult.ArticleSources?
                .OrderBy(a => a.SeqId)
                .ThenBy(a => a.DisplayName)
                .Select(a => new ArticleModel
                {
                    Title = a.ArticleTitle,
                    Url = a.FeedURL ?? a.OriginalSourceURL,
                    OriginalLanguage = a.OriginalLanguage,
                    PublishedDate = a.FeedPublishedDate,
                    SourceName = a.DisplayName
                }) ?? new ArticleModel[0];
        }

        private async Task<IEnumerable<GetAirportModel>> LoadSourceAirports(EventJoinResult eventJoinResult, EventConfig eventConfig)
        {
            return !eventJoinResult.IsModelNotRun
                ? await _airportService.GetSourceAirports(eventConfig.SourceAirportConfig)
                : new List<GetAirportModel>();
        }

        private async Task<IEnumerable<GetAirportModel>> LoadDestinationAirports(EventJoinResult eventJoinResult, EventConfig eventConfig)
        {
            return !eventJoinResult.IsModelNotRun
                ? await _airportService.GetDestinationAirports(eventConfig.DestinationAirportConfig)
                : new List<GetAirportModel>();
        }

        private async Task<OutbreakPotentialCategoryModel> LoadOutbreakPotential(int diseaseId, int geonameId)
        {
            return await _outbreakPotentialService.GetOutbreakPotentialByGeonameId(diseaseId, geonameId);
        }

        private RiskModel LoadExportationRisk(EventJoinResult eventJoinResult)
        {
            return new RiskModel
            {
                IsModelNotRun = eventJoinResult.IsModelNotRun,
                MinProbability = (float) (eventJoinResult.Event.EventExtensionSpreadMd?.MinExportationProbabilityViaAirports ?? 0),
                MaxProbability = (float) (eventJoinResult.Event.EventExtensionSpreadMd?.MaxExportationProbabilityViaAirports ?? 0),
                MinMagnitude = (float) (eventJoinResult.Event.EventExtensionSpreadMd?.MinExportationVolumeViaAirports ?? 0),
                MaxMagnitude = (float) (eventJoinResult.Event.EventExtensionSpreadMd?.MaxExportationVolumeViaAirports ?? 0)
            };
        }

        private RiskModel LoadImportationRisk(EventJoinResult eventJoinResult)
        {
            return new RiskModel
            {
                IsModelNotRun = eventJoinResult.IsModelNotRun,
                MinProbability = (float) (eventJoinResult.ImportationRisk?.MinProb ?? 0),
                MaxProbability = (float) (eventJoinResult.ImportationRisk?.MaxProb ?? 0),
                MinMagnitude = (float) (eventJoinResult.ImportationRisk?.MinVolume ?? 0),
                MaxMagnitude = (float) (eventJoinResult.ImportationRisk?.MaxVolume ?? 0)
            };
        }

        private IEnumerable<EventLocationModel> LoadEventLocations(IEnumerable<XtblEventLocationJoinResult> eventLocations, Dictionary<int, EventCaseCountModel> caseCountsFlattened)
        {
            return OrderingHelper.OrderLocationsByDefault(eventLocations.Select(l =>
            {
                var caseCount = caseCountsFlattened.First(c => c.Value.GeonameId == l.GeonameId).Value;
                return new EventLocationModel
                {
                    GeonameId = l.GeonameId,
                    LocationName = l.GeonameDisplayName,
                    ProvinceName = l.Admin1Name,
                    CountryName = l.CountryName,
                    LocationType = l.LocationType ?? (int) LocationType.Unknown,
                    EventDate = l.EventDate,
                    CaseCounts = new CaseCountModel
                    {
                        ReportedCases = caseCount.GetNestedRepCaseCount(),
                        ConfirmedCases = caseCount.GetNestedConfCaseCount(),
                        SuspectedCases = caseCount.GetNestedSuspCaseCount(),
                        Deaths = caseCount.GetNestedDeathCount(),
                        HasReportedCasesNesting = caseCount.HasRepCaseNestingApplied,
                        HasConfirmedCasesNesting = caseCount.HasConfCaseNestingApplied,
                        HasSuspectedCasesNesting = caseCount.HasSuspCaseNestingApplied,
                        HasDeathsNesting = caseCount.HasDeathNestingApplied
                    }
                };
            }));
        }

        private IEnumerable<EventLocationModel> LoadEventLocationsHistory(
            Dictionary<int, EventCaseCountModel> deltaCaseCounts,
            IEnumerable<XtblEventLocationJoinResult> eventLocations,
            IEnumerable<XtblEventLocationJoinResult> eventLocationsHistory)
        {
            return OrderingHelper.OrderLocationsByDefault(
                (
                    from delta in deltaCaseCounts.Values
                    join current in eventLocations on delta.GeonameId equals current.GeonameId
                    join previous in eventLocationsHistory on current.GeonameId equals previous.GeonameId into hl
                    from locJoin in hl.DefaultIfEmpty()
                    select new EventLocationModel
                    {
                        GeonameId = current.GeonameId,
                        LocationName = current.GeonameDisplayName,
                        ProvinceName = current.Admin1Name,
                        CountryName = current.CountryName,
                        LocationType = current.LocationType ?? (int) LocationType.Unknown,
                        EventDate = current.EventDate,
                        PreviousEventDate = locJoin?.EventDate,
                        CaseCounts = new CaseCountModel
                        {
                            ReportedCases = delta.GetNestedRepCaseCount(),
                            ConfirmedCases = delta.GetNestedConfCaseCount(),
                            SuspectedCases = delta.GetNestedSuspCaseCount(),
                            Deaths = delta.GetNestedDeathCount(),
                            HasReportedCasesNesting = delta.HasRepCaseNestingApplied,
                            HasConfirmedCasesNesting = delta.HasConfCaseNestingApplied,
                            HasSuspectedCasesNesting = delta.HasSuspCaseNestingApplied,
                            HasDeathsNesting = delta.HasDeathNestingApplied
                        }
                    }
                )
                .ToList());
        }

        private CaseCountModel LoadCaseCounts(Dictionary<int, EventCaseCountModel> caseCounts)
        {
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
        }

        private EventCalculationCasesModel LoadCalculationMetadata(List<EventSourceGridSpreadMd> eventSourceGrids)
        {
            return new EventCalculationCasesModel
            {
                CasesIncluded = eventSourceGrids.Sum(g => g.Cases),
                MinCasesIncluded = eventSourceGrids.Sum(g => g.MinCases),
                MaxCasesIncluded = eventSourceGrids.Sum(g => g.MaxCases)
            };
        }
    }
}