using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
using Biod.Insights.Api.Models.Article;
using Biod.Insights.Api.Models.Disease;
using Biod.Insights.Api.Models.Event;
using Biod.Insights.Api.Models.Geoname;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
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
            GetGeonameModel geoname = null;
            if (geonameId.HasValue)
            {
                geoname = await _geonameService.GetGeoname(geonameId.Value);
            }

            return new EventAirportModel
            {
                SourceAirports = await _airportService.GetSourceAirports(eventId),
                DestinationAirports = geoname != null ? await _airportService.GetDestinationAirports(eventId, geoname) : null
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
                eventQueryBuilder.SetDiseaseId(disease.Id);
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

            return new GetEventListModel
            {
                DiseaseInformation = disease,
                ImportationRisk = geoname != null && disease != null ? RiskCalculationHelper.CalculateImportationRisk(events) : null,
                ExportationRisk = disease != null ? RiskCalculationHelper.CalculateExportationRisk(events) : null,
                OutbreakPotentialCategory = outbreakPotentialCategory,
                EventsList = OrderingHelper.OrderEventsByDefault(events.Select(e => ConvertToModel(e, geoname))),
                CountryPins = await _mapService.GetCountryEventPins(new HashSet<int>(events.Select(e => e.Event.EventId)))
            };
        }

        public async Task<GetEventModel> GetEvent(int eventId, int? geonameId)
        {
            var eventQueryBuilder = new EventQueryBuilder(_biodZebraContext)
                .SetEventId(eventId)
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

            var @event = (await eventQueryBuilder.BuildAndExecute()).FirstOrDefault();

            if (@event == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested event with id {eventId} does not exist");
            }

            var diseaseId = @event.Event.DiseaseId.Value; // Disease Id can never be null

            var model = ConvertToModel(@event, geoname);

            // Compute remaining data that is only used for Event Details
            model.DiseaseInformation = await _diseaseService.GetDisease(diseaseId);
            model.SourceAirports = await _airportService.GetSourceAirports(eventId);
            model.DestinationAirports = await _airportService.GetDestinationAirports(eventId, geoname);
            if (geoname != null)
            {
                model.OutbreakPotentialCategory = await _outbreakPotentialService.GetOutbreakPotentialByGeonameId(diseaseId, geoname.GeonameId);
            }

            return model;
        }

        private GetEventModel ConvertToModel(EventJoinResult result, [AllowNull] GetGeonameModel geoname)
        {
            var eventLocations = result.Event.XtblEventLocation.ToList();
            var caseCounts = _caseCountService.GetCaseCountTree(eventLocations);
            var caseCountsFlattened = EventCaseCountModel.FlattenTree(caseCounts);

            return new GetEventModel
            {
                EventInformation = new EventInformationModel
                {
                    Id = result.Event.EventId,
                    Summary = result.Event.Summary,
                    Title = result.Event.EventTitle,
                    StartDate = result.Event.StartDate.Value, // Start date can never be null
                    EndDate = result.Event.EndDate,
                    LastUpdatedDate = result.Event.LastUpdatedDate.Value, // Last updated date can never be null
                    DiseaseId = result.Event.DiseaseId.Value  // Disease Id can never be null
                },
                ExportationRisk = new RiskModel
                {
                    IsModelNotRun = result.Event.IsLocalOnly,
                    MinProbability = (float) (result.ExportationRisk?.MinProb ?? 0),
                    MaxProbability = (float) (result.ExportationRisk?.MaxProb ?? 0),
                    MinMagnitude = (float) (result.ExportationRisk?.MinExpVolume ?? 0),
                    MaxMagnitude = (float) (result.ExportationRisk?.MaxExpVolume ?? 0)
                },
                ImportationRisk = geoname != null
                    ? new RiskModel
                    {
                        IsModelNotRun = result.Event.IsLocalOnly,
                        MinProbability = (float) (result.ImportationRisk?.MinProb ?? 0),
                        MaxProbability = (float) (result.ImportationRisk?.MaxProb ?? 0),
                        MinMagnitude = (float) (result.ImportationRisk?.MinVolume ?? 0),
                        MaxMagnitude = (float) (result.ImportationRisk?.MaxVolume ?? 0)
                    }
                    : null,
                IsLocal = geoname != null && result.ImportationRisk?.LocalSpread == 1,
                Articles = result.Event.XtblArticleEvent
                    .Select(x => x.Article)
                    .OrderBy(ArticleHelper.GetSeqId)
                    .ThenBy(a => a.ArticleFeed.DisplayName)
                    .ThenBy(a => a.ArticleFeed.FullName)
                    .Select(a => new ArticleModel
                    {
                        Title = a.ArticleTitle,
                        Url = a.FeedUrl ?? a.OriginalSourceUrl,
                        OriginalLanguage = a.OriginalLanguage,
                        PublishedDate = a.FeedPublishedDate,
                        SourceName = ArticleHelper.GetDisplayName(a)
                    }),
                EventLocations = OrderingHelper.OrderLocationsByDefault(eventLocations.Select(l =>
                {
                    var caseCount = caseCountsFlattened.First(c => c.Value.GeonameId == l.GeonameId).Value;
                    return new EventLocationModel
                    {
                        GeonameId = l.GeonameId,
                        LocationName = l.Geoname.DisplayName,
                        ProvinceName = result.GeonamesLookup[l.Geoname.Admin1GeonameId ?? -1].Name,
                        CountryName = l.Geoname.CountryName,
                        LocationType = l.Geoname.LocationType ?? (int) Constants.LocationType.Unknown,
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
                }
            };
        }
    }
}