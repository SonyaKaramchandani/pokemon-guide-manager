using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
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

        /// <summary>
        /// Event service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseService">the disease service</param>
        /// <param name="geonameService">the geoname service</param>
        /// <param name="outbreakPotentialService">the outbreak potential service</param>
        public EventService(
            BiodZebraContext biodZebraContext,
            ILogger<EventService> logger,
            IDiseaseService diseaseService,
            IGeonameService geonameService,
            IOutbreakPotentialService outbreakPotentialService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseService = diseaseService;
            _geonameService = geonameService;
            _outbreakPotentialService = outbreakPotentialService;
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
            GetGeonameModel geoname = null;
            if (geonameId.HasValue)
            {
                geoname = await _geonameService.GetGeoname(geonameId.Value);
            }

            DiseaseInformationModel disease = null;
            if (diseaseId.HasValue)
            {
                disease = await _diseaseService.GetDisease(diseaseId.Value);
            }

            var events = await _biodZebraContext.usp_ZebraEventGetEventSummary_Result
                .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventGetEventSummary
                                            @UserId = {""},
		                                    @GeonameIds = {(geoname != null ? geoname.GeonameId.ToString() : "")},
		                                    @DiseasesIds = {(disease != null ? disease.Id.ToString() : "")},
		                                    @TransmissionModesIds = {""},
		                                    @InterventionMethods = {""},
		                                    @SeverityRisks = {""},
		                                    @BiosecurityRisks = {""},
		                                    @LocationOnly = {0}")
                .ToListAsync();

            OutbreakPotentialCategoryModel outbreakPotentialCategory = null;
            if (geoname != null && disease != null)
            {
                outbreakPotentialCategory = (await _outbreakPotentialService.GetOutbreakPotentialByGeoname(geoname)).FirstOrDefault(o => o.DiseaseId == disease.Id);
            }

            return new GetEventListModel
            {
                DiseaseInformation = disease,
                ImportationRisk = geoname != null ? RiskCalculationHelper.CalculateImportationRiskCompat(events) : null,
                ExportationRisk = RiskCalculationHelper.CalculateExportationRiskCompat(events),
                OutbreakPotentialCategory = outbreakPotentialCategory,
                EventsList = events.Select(e => new GetEventModel
                {
                    EventInformation = new EventInformationModel
                    {
                        Id = e.EventId,
                        Summary = e.Summary,
                        Title = e.EventTitle,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        LastUpdatedDate = e.LastUpdatedDate,
                        OutbreakLocation = e.CountryName
                    },
                    ExportationRisk = new RiskModel
                    {
                        MinProbability = (float) (e.ExportationProbabilityMin ?? 0),
                        MaxProbability = (float) (e.ExportationProbabilityMax ?? 0),
                        MinMagnitude = (float) (e.ExportationInfectedTravellersMin ?? 0),
                        MaxMagnitude = (float) (e.ExportationInfectedTravellersMax ?? 0)
                    },
                    ImportationRisk = geoname != null
                        ? new RiskModel
                        {
                            MinProbability = (float) (e.ImportationMinProbability ?? 0),
                            MaxProbability = (float) (e.ImportationMaxProbability ?? 0),
                            MinMagnitude = (float) (e.ImportationInfectedTravellersMin ?? 0),
                            MaxMagnitude = (float) (e.ImportationInfectedTravellersMax ?? 0)
                        }
                        : null,
                    IsLocal = geoname != null && e.LocalSpread == 1,
                    CaseCounts = new CaseCountModel
                    {
                        ReportedCases = e.RepCases ?? 0,
                        Deaths = e.Deaths ?? 0
                    }
                })
            };
        }

        public async Task<GetEventModel> GetEvent(int eventId, int? geonameId)
        {
            var eventRisk = (await _biodZebraContext.usp_ZebraEventGetEventSummary_Result
                    .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventGetEventSummaryByEventId
                                            @UserId = {""},
		                                    @GeonameIds = {(geonameId.HasValue ? geonameId.Value.ToString() : "")},
		                                    @EventId = {eventId}")
                    .ToListAsync())
                .FirstOrDefault();

            var @event = await _biodZebraContext.Event
                .Include(e => e.XtblArticleEvent)
                .ThenInclude(x => x.Article)
                .Where(e => e.EventId == eventId)
                .FirstOrDefaultAsync();

            if (@event == null || eventRisk == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested event with id {eventId} does not exist");
            }

            return new GetEventModel
            {
                EventInformation = new EventInformationModel
                {
                    Id = @event.EventId,
                    Summary = @event.Summary,
                    Title = @event.EventTitle,
                    StartDate = @event.StartDate.Value, // Start date can never be null
                    EndDate = @event.EndDate,
                    LastUpdatedDate = @event.LastUpdatedDate.Value, // Last updated date can never be null
                    OutbreakLocation = eventRisk.CountryName
                },
                ExportationRisk = new RiskModel
                {
                    MinProbability = (float) (eventRisk.ExportationProbabilityMin ?? 0),
                    MaxProbability = (float) (eventRisk.ExportationProbabilityMax ?? 0),
                    MinMagnitude = (float) (eventRisk.ExportationInfectedTravellersMin ?? 0),
                    MaxMagnitude = (float) (eventRisk.ExportationInfectedTravellersMax ?? 0)
                },
                ImportationRisk = geonameId.HasValue
                    ? new RiskModel
                    {
                        MinProbability = (float) (eventRisk.ImportationMinProbability ?? 0),
                        MaxProbability = (float) (eventRisk.ImportationMaxProbability ?? 0),
                        MinMagnitude = (float) (eventRisk.ImportationInfectedTravellersMin ?? 0),
                        MaxMagnitude = (float) (eventRisk.ImportationInfectedTravellersMax ?? 0)
                    }
                    : null,
                IsLocal = geonameId.HasValue && eventRisk.LocalSpread == 1,
                CaseCounts = await GetEventCaseCount(eventId)
            };
        }
    }
}