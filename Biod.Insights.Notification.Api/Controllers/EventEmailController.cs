using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Notification.Engine.Services.NewEvent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Notification.Api.Controllers
{
    [ApiController]
    [Route("api/notification/event")]
    public class EventEmailController : ControllerBase
    {
        private readonly ILogger<EventEmailController> _logger;
        private readonly INewEventNotificationService _newEventNotificationService;
        
        public EventEmailController(ILogger<EventEmailController> logger, INewEventNotificationService newEventNotificationService)
        {
            _logger = logger;
            _newEventNotificationService = newEventNotificationService;
        }

        [HttpPost]
        public async Task SendEmail([Required] [FromQuery] int eventId)
        {
            await _newEventNotificationService.ProcessRequest(eventId);
        }
    }
}