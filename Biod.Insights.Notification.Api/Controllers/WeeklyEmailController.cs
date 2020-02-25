using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Notification.Api.Controllers
{
    [ApiController]
    [Route("api/notification/weekly")]
    public class WeeklyEmailController : ControllerBase
    {
        private readonly ILogger<WeeklyEmailController> _logger;
        
        public WeeklyEmailController(ILogger<WeeklyEmailController> logger)
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