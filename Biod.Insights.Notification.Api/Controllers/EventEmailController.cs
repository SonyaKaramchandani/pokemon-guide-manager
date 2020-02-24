using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Notification.Api.Controllers
{
    [ApiController]
    [Route("api/notification/event")]
    public class EventEmailController : ControllerBase
    {
        private readonly ILogger<EventEmailController> _logger;
        
        public EventEmailController(ILogger<EventEmailController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task SendEmail()
        {
            return;
        }
    }
}