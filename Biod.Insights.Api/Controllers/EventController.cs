using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Event;
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
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;
        private readonly IUserService _userService;

        public EventController(
            ILogger<EventController> logger,
            IEventService eventService,
            IDiseaseRelevanceService diseaseRelevanceService,
            IUserService userService)
        {
            _logger = logger;
            _eventService = eventService;
            _diseaseRelevanceService = diseaseRelevanceService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int? diseaseId = null, [FromQuery] int? geonameId = null)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            GetEventListModel result;
            if (!diseaseId.HasValue && !string.IsNullOrWhiteSpace(tokenUserId))
            {
                var user = await _userService.GetUser(tokenUserId);
                result = await _eventService.GetEvents(geonameId, user.DiseaseRelevanceSetting);
            }
            else
            {
                result = await _eventService.GetEvents(diseaseId, geonameId);
            }

            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent([Required] int eventId, [FromQuery] int? geonameId = null)
        {
            var result = await _eventService.GetEvent(eventId, geonameId);
            return Ok(result);
        }

        [HttpGet("{eventId}/airport")]
        public async Task<IActionResult> GetAirports([Required] int eventId, [FromQuery] int? geonameId = null)
        {
            var result = await _eventService.GetAirports(eventId, geonameId);
            return Ok(result);
        }
    }
}