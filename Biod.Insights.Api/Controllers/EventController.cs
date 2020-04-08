using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Common;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEnvironmentVariables _environmentVariables;
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IRiskCalculationService _riskCalculationService;

        public EventController(
            ILogger<EventController> logger,
            IEnvironmentVariables environmentVariables,
            IEventService eventService,
            IUserService userService,
            IRiskCalculationService riskCalculationService)
        {
            _logger = logger;
            _environmentVariables = environmentVariables;
            _eventService = eventService;
            _userService = userService;
            _riskCalculationService = riskCalculationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int? diseaseId = null, [FromQuery] int? geonameId = null)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);

            var eventConfigBuilder = new EventConfig.Builder()
                .ShouldIncludeExportationRisk()
                .ShouldIncludeArticles()
                .ShouldIncludeLocations();

            if (diseaseId.HasValue)
            {
                eventConfigBuilder.AddDiseaseId(diseaseId.Value);
            }

            if (geonameId.HasValue)
            {
                eventConfigBuilder.ShouldIncludeImportationRisk(geonameId.Value);
                eventConfigBuilder.ShouldIncludeLocalCaseCount(geonameId.Value);
            }

            GetEventListModel result;
            if (!string.IsNullOrWhiteSpace(tokenUserId))
            {
                var user = await _userService.GetUser(new UserConfig.Builder().SetUserId(tokenUserId).Build());
                result = await _eventService.GetEvents(eventConfigBuilder.Build(), user.DiseaseRelevanceSetting);
            }
            else
            {
                result = await _eventService.GetEvents(eventConfigBuilder.Build());
            }

            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent([Required] int eventId, [FromQuery] int? geonameId = null)
        {
            var eventConfigBuilder = new EventConfig.Builder()
                .ShouldIncludeDiseaseInformation()
                .ShouldIncludeArticles()
                .ShouldIncludeLocations()
                .ShouldIncludeExportationRisk()
                .ShouldIncludeCalculationMetadata()
                .ShouldIncludeSourceAirports(new SourceAirportConfig.Builder(eventId)
                    .ShouldIncludePopulation()
                    .ShouldIncludeCaseCounts()
                    .ShouldIncludeCity()
                    .Build())
                .ShouldIncludeDestinationAirports(new AirportConfig.Builder(eventId)
                    .ShouldIncludeCity()
                    .ShouldIncludeImportationRisk(geonameId)
                    .Build())
                .SetEventId(eventId);

            if (geonameId.HasValue)
            {
                eventConfigBuilder
                    .ShouldIncludeImportationRisk(geonameId.Value)
                    .ShouldIncludeLocalCaseCount(geonameId.Value);
            }

            var result = await _eventService.GetEvent(eventConfigBuilder.Build());
            return Ok(result);
        }

        [HttpGet("{eventId}/airport")]
        public async Task<IActionResult> GetAirports([Required] int eventId, [FromQuery] int? geonameId = null)
        {
            var result = await _eventService.GetAirports(eventId, geonameId);
            return Ok(result);
        }

        [HttpGet("{eventId}/riskmodel")]
        public async Task<IActionResult> GetCalculationBreakdown([Required] int eventId, [FromQuery] int? geonameId = null)
        {
            var result = await _riskCalculationService.GetCalculationBreakdown(eventId, geonameId);
            return Ok(result);
        }
    }
}