using System.Threading.Tasks;
using Biod.Insights.Notification.Engine.Services.Weekly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Notification.Api.Controllers
{
    [ApiController]
    [Route("api/notification/weekly")]
    public class WeeklyEmailController : ControllerBase
    {
        private readonly ILogger<WeeklyEmailController> _logger;
        private readonly IWeeklyNotificationService _weeklyNotificationService;
        
        public WeeklyEmailController(ILogger<WeeklyEmailController> logger, IWeeklyNotificationService weeklyNotificationService)
        {
            _logger = logger;
            _weeklyNotificationService = weeklyNotificationService;
        }

        [HttpPost]
        public async Task SendEmail()
        {
            await _weeklyNotificationService.ProcessRequest();
        }
    }
}