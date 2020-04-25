using Biod.Insights.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/app")]
    public class AppMetadataController : ControllerBase
    {
        private readonly ILogger<AppMetadataController> _logger;
        private readonly IAppMetadataService _appMetadataService;

        public AppMetadataController(ILogger<AppMetadataController> logger, IAppMetadataService appMetadataService)
        {
            _logger = logger;
            _appMetadataService = appMetadataService;
        }

        [HttpGet("metadata")]
        public ActionResult GetAppMetadata()
        {
            var result = _appMetadataService.GetMetadata();
            return Ok(result);
        }
    }
}