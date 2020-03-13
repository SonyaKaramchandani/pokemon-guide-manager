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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<EventAirportModel> GetAirports(int eventId)
        {
            return new EventAirportModel
            {
                SourceAirports = await _airportService.GetSourceAirports(eventId),
                DestinationAirports = await _airportService.GetDestinationAirports(eventId)
            };
        }

        public async Task<Dictionary<string, Dictionary<int, HashSet<int>>>> GetUsersAffectedByEvent(int eventId)
        {
            var eventData = await GetEvent(eventId, null, false);
            return await GetUsersAffectedByEvent(eventData);
        }

        public async Task<Dictionary<string, Dictionary<int, HashSet<int>>>> GetUsersAffectedByEvent(GetEventModel eventModel)
        {
            var eventLocations = new Dictionary<string, Dictionary<int, HashSet<int>>>();
            foreach (var eventLocation in eventModel.EventLocations)
            {
                var users = await _biodZebraContext.ufn_ZebraGetLocalUserLocationsByGeonameId_Result
                    .FromSqlInterpolated(
                        $@"SELECT DISTINCT UserId, UserGeonameId FROM bd.ufn_ZebraGetLocalUserLocationsByGeonameId({eventLocation.GeonameId}, 1, 1, 1, {eventModel.EventInformation.DiseaseId})")
                    .ToListAsync();

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

        public async Task<GetEventListModel> GetEvents(HashSet<int> diseaseIds, int? geonameId)
        {
            var eventQueryBuilder = new EventQueryBuilder(_biodZebraContext)
                .AddDiseaseIds(diseaseIds)
                .IncludeExportationRisk()
                .IncludeLocations()
                .IncludeArticles();

            GetGeonameModel geoname = null;
            if (geonameId.HasValue)
            {
                // Importation risk required
                geoname = await _geonameService.GetGeoname(geonameId.Value);
                eventQueryBuilder.IncludeImportationRisk(geonameId.Value);
            }

            var events = (await eventQueryBuilder.BuildAndExecute()).ToList();
            var eventModels = new List<GetEventModel>();
            foreach (var e in events)
            {
                eventModels.Add(await ConvertToModel(e, geoname));
            }

            return new GetEventListModel
            {
                EventsList = OrderingHelper.OrderEventsByDefault(eventModels),
                CountryPins = await _mapService.GetCountryEventPins(new HashSet<int>(events.Select(e => e.Event.EventId)))
            };
        }

        public async Task<GetEventListModel> GetEvents(int? diseaseId, int? geonameId)
        {
            var eventQueryBuilder = new EventQueryBuilder(_biodZebraContext)
                .IncludeExportationRisk()
                .IncludeLocations()
                .IncludeArticles();

            DiseaseInformationModel disease = null;
            if (diseaseId.HasValue)
            {
                // Disease filtering active
                disease = await _diseaseService.GetDisease(diseaseId.Value);
                eventQueryBuilder.AddDiseaseId(disease.Id);
            }

            GetGeonameModel geoname = null;
            if (geonameId.HasValue)
            {
                // Importation risk required
                geoname = await _geonameService.GetGeoname(geonameId.Value);
                eventQueryBuilder.IncludeImportationRisk(geonameId.Value);
            }

            OutbreakPotentialCategoryModel outbreakPotentialCategory = null;
            if (geoname != null && disease != null)
            {
                outbreakPotentialCategory = await _outbreakPotentialService.GetOutbreakPotentialByGeoname(disease.Id, geoname);
            }

            var events = (await eventQueryBuilder.BuildAndExecute()).ToList();
            var eventModels = new List<GetEventModel>();
            foreach (var e in events)
            {
                eventModels.Add(await ConvertToModel(e, geoname));
            }

            return new GetEventListModel
            {
                DiseaseInformation = disease,
                LocalCaseCounts = geoname != null && disease != null ? await _diseaseService.GetDiseaseCaseCount(disease.Id, geoname.GeonameId) : null,
                ImportationRisk = geoname != null && disease != null ? RiskCalculationHelper.CalculateImportationRisk(events) : null,
                ExportationRisk = disease != null ? RiskCalculationHelper.CalculateExportationRisk(events) : null,
                OutbreakPotentialCategory = outbreakPotentialCategory,
                EventsList = OrderingHelper.OrderEventsByDefault(eventModels),
                CountryPins = await _mapService.GetCountryEventPins(new HashSet<int>(events.Select(e => e.Event.EventId)))
            };
        }

        public async Task<GetEventListModel> GetEvents(int? diseaseId, int? geonameId, DiseaseRelevanceSettingsModel relevanceSettings)
        {
            var diseaseIds = relevanceSettings.GetRelevantDiseases();
            DiseaseInformationModel disease = null;
            if (diseaseId.HasValue)
            {
                // Check whether disease exists
                disease = await _diseaseService.GetDisease(diseaseId.Value);
                diseaseIds.IntersectWith(new [] {disease.Id});
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
            
            var result = await GetEvents(diseaseIds, geonameId);
            if (disease != null && geonameId.HasValue)
            {
                // A single disease id was queried, populate with relevant fields
                result.LocalCaseCounts = await _diseaseService.GetDiseaseCaseCount(disease.Id, geonameId.Value);
            }
            return result;
        }

        public async Task<GetEventModel> GetEvent(int eventId, int? geonameId, bool includeLocationHistory)
        {
            var eventQueryBuilder = new EventQueryBuilder(_biodZebraContext)
                .SetEventId(eventId)
                .IncludeExportationRisk()
                .IncludeLocations()
                .IncludeArticles();

            if (includeLocationHistory)
            {
                eventQueryBuilder.IncludeLocationsHistory();
            }

            GetGeonameModel geoname = null;
            if (geonameId.HasValue)
            {
                // Importation risk required
                geoname = await _geonameService.GetGeoname(geonameId.Value);
                eventQueryBuilder.IncludeImportationRisk(geonameId.Value);
            }

            var @event = (await eventQueryBuilder.BuildAndExecute()).FirstOrDefault();

            if (@event == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested event with id {eventId} does not exist");
            }

            var diseaseId = @event.Event.DiseaseId;

            var model = await ConvertToModel(@event, geoname);

            // Compute remaining data that is only used for Event Details
            model.DiseaseInformation = await _diseaseService.GetDisease(diseaseId);
            model.SourceAirports = !@event.IsModelNotRun ? await _airportService.GetSourceAirports(eventId) : new List<GetAirportModel>();
            model.DestinationAirports = !@event.IsModelNotRun ? await _airportService.GetDestinationAirports(eventId) : new List<GetAirportModel>();
            if (geoname != null)
            {
                model.OutbreakPotentialCategory = await _outbreakPotentialService.GetOutbreakPotentialByGeonameId(diseaseId, geoname.GeonameId);
            }

            return model;
        }

        private async Task<GetEventModel> ConvertToModel(EventJoinResult result, [AllowNull] GetGeonameModel geoname)
        {
            // Latest case counts
            var eventLocations = result.XtblEventLocations.ToList();
            var caseCounts = _caseCountService.GetCaseCountTree(eventLocations);
            var caseCountsFlattened = EventCaseCountModel.FlattenTree(caseCounts);

            // Historical case counts (1 row behind)
            List<EventLocationModel> updatedLocations = null;
            if (result.XtblEventLocationsHistory != null)
            {
                var eventLocationsHistory = result.XtblEventLocationsHistory.ToList();
                var caseCountsHistory = _caseCountService.GetCaseCountTree(eventLocationsHistory);
                var caseCountsHistoryFlattened = EventCaseCountModel.FlattenTree(caseCountsHistory);

                // Increased case counts
                var deltaCaseCounts = _caseCountService.GetIncreasedCaseCount(caseCountsHistoryFlattened, caseCountsFlattened)
                    .Where(c => c.Value.RawRepCaseCount > 0)
                    .ToDictionary(c => c.Key, c => c.Value);
                var deltaCaseCountsFlattened = EventCaseCountModel.FlattenTree(deltaCaseCounts);
                updatedLocations = (
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
                        LocationType = current.LocationType ?? (int)LocationType.Unknown,
                        EventDate = locJoin?.EventDate,
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
                .ToList();
            }

            var countryOnlyLocations = result.XtblEventLocations.All(x => x.LocationType == (int) LocationType.Country);
            var localCaseCount = geoname != null ? await _diseaseService.GetDiseaseCaseCount(result.Event.DiseaseId, geoname?.GeonameId, result.Event.EventId) : null;

            return new GetEventModel
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
                ExportationRisk = new RiskModel
                {
                    IsModelNotRun = result.IsModelNotRun,
                    MinProbability = (float) (result.Event.EventExtension?.MinExportationProbabilityViaAirports ?? 0),
                    MaxProbability = (float) (result.Event.EventExtension?.MaxExportationProbabilityViaAirports ?? 0),
                    MinMagnitude = (float) (result.Event.EventExtension?.MinExportationVolumeViaAirports ?? 0),
                    MaxMagnitude = (float) (result.Event.EventExtension?.MaxExportationVolumeViaAirports ?? 0)
                },
                ImportationRisk = geoname != null
                    ? new RiskModel
                    {
                        IsModelNotRun = result.IsModelNotRun,
                        MinProbability = (float) (result.ImportationRisk?.MinProb ?? 0),
                        MaxProbability = (float) (result.ImportationRisk?.MaxProb ?? 0),
                        MinMagnitude = (float) (result.ImportationRisk?.MinVolume ?? 0),
                        MaxMagnitude = (float) (result.ImportationRisk?.MaxVolume ?? 0)
                    }
                    : null,
                IsLocal = geoname != null && localCaseCount?.ReportedCases > 0,
                LocalCaseCounts = localCaseCount,
                Articles = result.ArticleSources?
                               .OrderBy(a => a.SeqId)
                               .ThenBy(a => a.DisplayName)
                               .Select(a => new ArticleModel
                               {
                                   Title = a.ArticleTitle,
                                   Url = a.FeedURL ?? a.OriginalSourceURL,
                                   OriginalLanguage = a.OriginalLanguage,
                                   PublishedDate = a.FeedPublishedDate,
                                   SourceName = a.DisplayName
                               }) ?? new ArticleModel[0],
                EventLocations = OrderingHelper.OrderLocationsByDefault(eventLocations.Select(l =>
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
                })),
                UpdatedLocations = updatedLocations != null ? OrderingHelper.OrderLocationsByDefault(updatedLocations) : null,
                CaseCounts = new CaseCountModel
                {
                    ReportedCases = caseCounts.Sum(c => c.Value.GetNestedRepCaseCount()),
                    ConfirmedCases = caseCounts.Sum(c => c.Value.GetNestedConfCaseCount()),
                    SuspectedCases = caseCounts.Sum(c => c.Value.GetNestedSuspCaseCount()),
                    Deaths = caseCounts.Sum(c => c.Value.GetNestedDeathCount()),
                    HasReportedCasesNesting = caseCounts.Any(c => c.Value.HasRepCaseNestingApplied),
                    HasConfirmedCasesNesting = caseCounts.Any(c => c.Value.HasConfCaseNestingApplied),
                    HasSuspectedCasesNesting = caseCounts.Any(c => c.Value.HasSuspCaseNestingApplied),
                    HasDeathsNesting = caseCounts.Any(c => c.Value.HasDeathNestingApplied)
                },
                DiseaseInformation = new DiseaseInformationModel
                {
                    Id = result.Event.DiseaseId
                }
            };
        }
    }
}