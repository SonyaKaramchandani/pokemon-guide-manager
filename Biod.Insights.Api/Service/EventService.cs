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
using Biod.Insights.Api.Models.Map;
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
        public EventService(
            BiodZebraContext biodZebraContext,
            ILogger<EventService> logger,
            IDiseaseService diseaseService,
            IGeonameService geonameService,
            IOutbreakPotentialService outbreakPotentialService,
            IAirportService airportService,
            IMapService mapService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseService = diseaseService;
            _geonameService = geonameService;
            _outbreakPotentialService = outbreakPotentialService;
            _airportService = airportService;
            _mapService = mapService;
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

        public async Task<CaseCountModel> GetEventCaseCount(int eventId)
        {
            var @event = (await _biodZebraContext.usp_ZebraEventGetCaseCountByEventId_Result
                    .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventGetCaseCountByEventId
                                            @EventId = {eventId}")
                    .ToListAsync())
                .First(e => e.GeonameId == -1);

            if (@event.ConfCases == null
                || @event.SuspCases == null
                || @event.RepCases == null
                || @event.Deaths == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested event with id {eventId} does not exist");
            }

            return new CaseCountModel
            {
                ConfirmedCases = @event.ConfCases.Value,
                SuspectedCases = @event.SuspCases.Value,
                ReportedCases = @event.RepCases.Value,
                Deaths = @event.Deaths.Value
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
                outbreakPotentialCategory = (await _outbreakPotentialService.GetOutbreakPotentialByGeoname(geoname)).FirstOrDefault(o => o.DiseaseId == disease.Id);
            }

            var events = (await eventQueryBuilder.BuildAndExecute()).ToList();

            return new GetEventListModel
            {
                DiseaseInformation = disease,
                ImportationRisk = geoname != null && disease != null ? RiskCalculationHelper.CalculateImportationRisk(events) : null,
                ExportationRisk = disease != null ? RiskCalculationHelper.CalculateExportationRisk(events) : null,
                OutbreakPotentialCategory = outbreakPotentialCategory,
                EventsList = events.Select(e => ConvertToModel(e, geoname)),
                CountryPins = _mapService.GetCountryEventPins(new HashSet<int>(events.Select(e => e.Event.EventId)))
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

            return ConvertToModel(@event, geoname);
        }

        private GetEventModel ConvertToModel(EventJoinResult result, [AllowNull] GetGeonameModel geoname)
        {
            return new GetEventModel
            {
                EventInformation = new EventInformationModel
                {
                    Id = result.Event.EventId,
                    Summary = result.Event.Summary,
                    Title = result.Event.EventTitle,
                    StartDate = result.Event.StartDate.Value, // Start date can never be null
                    EndDate = result.Event.EndDate,
                    LastUpdatedDate = result.Event.LastUpdatedDate.Value // Last updated date can never be null
                },
                ExportationRisk = new RiskModel
                {
                    ModelNotRun = result.Event.IsLocalOnly,
                    MinProbability = (float) (result.ExportationRisk?.MinProb ?? 0),
                    MaxProbability = (float) (result.ExportationRisk?.MaxProb ?? 0),
                    MinMagnitude = (float) (result.ExportationRisk?.MinExpVolume ?? 0),
                    MaxMagnitude = (float) (result.ExportationRisk?.MaxExpVolume ?? 0)
                },
                ImportationRisk = geoname != null
                    ? new RiskModel
                    {
                        ModelNotRun = result.Event.IsLocalOnly,
                        MinProbability = (float) (result.ImportationRisk?.MinProb ?? 0),
                        MaxProbability = (float) (result.ImportationRisk?.MaxProb ?? 0),
                        MinMagnitude = (float) (result.ImportationRisk?.MinVolume ?? 0),
                        MaxMagnitude = (float) (result.ImportationRisk?.MaxVolume ?? 0)
                    }
                    : null,
                IsLocal = geoname != null && result.ImportationRisk?.LocalSpread == 1,
                Articles = result.Event.XtblArticleEvent.Select(x => new ArticleModel
                {
                    Title = x.Article.ArticleTitle,
                    Url = x.Article.FeedUrl ?? x.Article.OriginalSourceUrl,
                    OriginalLanguage = x.Article.OriginalLanguage,
                    PublishedDate = x.Article.FeedPublishedDate,
                    SourceName = x.Article.ArticleFeed.DisplayName // TODO: Get name using logic from [usp_ZebraEventGetArticlesByEventId]
                }),
                // TODO:
                // CaseCounts = 
                // EventLocations = 
            };
        }
    }
}