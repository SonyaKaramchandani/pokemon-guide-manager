using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Notification.Engine.Services.Proximal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Notification.Api.Controllers
{
    [ApiController]
    [Route("api/notification/proximal")]
    public class ProximalEmailController : ControllerBase
    {
        private readonly ILogger<ProximalEmailController> _logger;
        private readonly IProximalNotificationService _proximalNotificationService;
        
        public ProximalEmailController(ILogger<ProximalEmailController> logger, IProximalNotificationService proximalNotificationService)
        {
            _logger = logger;
            _proximalNotificationService = proximalNotificationService;
        }

        [HttpPost]
        public async Task SendEmail([Required] [FromQuery] int eventId)
        {
            await _proximalNotificationService.ProcessRequest(eventId);
        }
    }
}