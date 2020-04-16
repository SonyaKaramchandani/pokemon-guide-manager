using System.ComponentModel.DataAnnotations;
using Biod.Insights.Service.Models.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/log")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        [HttpPost("js")]
        public ActionResult SaveJsLog([FromBody] [Required] JsLogModel logModel)
        {
            switch (logModel.LogLevel.ToLowerInvariant())
            {
                case "critical":
                case "fatal":
                    _logger.LogCritical(logModel.Message);
                    break;
                case "error":
                case "exception":
                    _logger.LogError(logModel.Message);
                    break;
                case "warning":
                case "warn":
                    _logger.LogWarning(logModel.Message);
                    break;
                case "information":
                case "info":
                    _logger.LogInformation(logModel.Message);
                    break;
                default:
                    _logger.LogDebug(logModel.Message);
                    break;
            }

            return Ok();
        }
    }
}