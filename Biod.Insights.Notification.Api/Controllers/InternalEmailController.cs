using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Notification.Engine.Models;
using Biod.Insights.Notification.Engine.Services.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Notification.Api.Controllers
{
    [ApiController]
    [Route("api/notification/internal")]
    public class InternalEmailController : ControllerBase
    {
        private readonly ILogger<InternalEmailController> _logger;
        private readonly IInternalNotificationService _internalNotificationService;
        
        public InternalEmailController(ILogger<InternalEmailController> logger, IInternalNotificationService internalNotificationService)
        {
            _logger = logger;
            _internalNotificationService = internalNotificationService;
        }

        [HttpPost]
        public async Task SendEmail([FromBody] [Required] InternalEmailViewModel emailViewModel)
        {
            await _internalNotificationService.ProcessRequest(emailViewModel.Subject, emailViewModel.Body);
        }
    }
}