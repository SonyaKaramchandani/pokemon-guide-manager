using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _eventService;

        public EventController(ILogger<EventController> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int? diseaseId = null, [FromQuery] int? geonameId = null)
        {
            var result = await _eventService.GetEvents(diseaseId, geonameId);
            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent([Required] int eventId, [FromQuery] int? geonameId = null)
        {
            var result = await _eventService.GetEvent(eventId, geonameId);
            return Ok(result);
        }

        [HttpGet("{eventId}/casecount")]
        public async Task<IActionResult> GetEventCaseCount([Required] int eventId)
        {
            var result = await _eventService.GetEventCaseCount(eventId);
            return Ok(result);
        }
    }
}