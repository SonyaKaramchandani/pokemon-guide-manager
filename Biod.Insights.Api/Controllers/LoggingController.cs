using System.ComponentModel.DataAnnotations;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Models.Logging;
using Biod.Products.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/log")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;
        private readonly IEnvironmentVariables _environmentVariables;

        public LoggingController(ILogger<LoggingController> logger, IEnvironmentVariables environmentVariables)
        {
            _logger = logger;
            _environmentVariables = environmentVariables;
        }

        [HttpPost("js")]
        public ActionResult SaveJsLog([FromBody] [Required] JsLogModel logModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables).DefaultIfWhiteSpace("Unknown User");
            switch (logModel.LogLevel.ToLowerInvariant())
            {
                case "critical":
                case "fatal":
                    _logger.LogCritical($"User [{tokenUserId}]: {logModel.Message}");
                    break;
                case "error":
                case "exception":
                    _logger.LogError($"User [{tokenUserId}]: {logModel.Message}");
                    break;
                case "warning":
                case "warn":
                    _logger.LogWarning($"User [{tokenUserId}]: {logModel.Message}");
                    break;
                case "information":
                case "info":
                    _logger.LogInformation($"User [{tokenUserId}]: {logModel.Message}");
                    break;
                default:
                    _logger.LogDebug($"User [{tokenUserId}]: {logModel.Message}");
                    break;
            }

            return Ok();
        }
    }
}