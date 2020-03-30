using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public EventController(
            ILogger<EventController> logger,
            IEventService eventService,
            IUserService userService)
        {
            _logger = logger;
            _eventService = eventService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int? diseaseId = null, [FromQuery] int? geonameId = null)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);

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
            }

            GetEventListModel result;
            if (!string.IsNullOrWhiteSpace(tokenUserId))
            {
                var user = await _userService.GetUser(tokenUserId);
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
            var eventBuilder = new EventConfig.Builder()
                .ShouldIncludeDiseaseInformation()
                .ShouldIncludeArticles()
                .ShouldIncludeLocations()
                .ShouldIncludeExportationRisk()
                .ShouldIncludeSourceAirports(new AirportConfig.Builder(eventId).ShouldIncludeCity().Build())
                .ShouldIncludeDestinationAirports(new AirportConfig.Builder(eventId).ShouldIncludeCity().Build())
                .SetEventId(eventId);

            if (geonameId.HasValue)
            {
                eventBuilder.ShouldIncludeImportationRisk(geonameId.Value);
            }

            var result = await _eventService.GetEvent(eventBuilder.Build());
            return Ok(result);
        }

        [HttpGet("{eventId}/airport")]
        public async Task<IActionResult> GetAirports([Required] int eventId)
        {
            var result = await _eventService.GetAirports(eventId);
            return Ok(result);
        }
    }
}