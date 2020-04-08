using System.Threading.Tasks;
using Biod.Insights.Common;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Disease;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/diseaserisk")]
    public class DiseaseRiskController : ControllerBase
    {
        private readonly ILogger<DiseaseRiskController> _logger;
        private readonly IEnvironmentVariables _environmentVariables;
        private readonly IDiseaseRiskService _diseaseRiskService;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;
        private readonly IUserService _userService;

        public DiseaseRiskController(
            ILogger<DiseaseRiskController> logger,
            IEnvironmentVariables environmentVariables,
            IDiseaseRiskService diseaseRiskService,
            IDiseaseRelevanceService diseaseRelevanceService,
            IUserService userService)
        {
            _logger = logger;
            _environmentVariables = environmentVariables;
            _diseaseRiskService = diseaseRiskService;
            _diseaseRelevanceService = diseaseRelevanceService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRiskForLocation([FromQuery] int? geonameId = null)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
            RiskAggregationModel result;
            if (!string.IsNullOrWhiteSpace(tokenUserId))
            {
                var user = await _userService.GetUser(new UserConfig.Builder().SetUserId(tokenUserId).Build());
                result = await _diseaseRiskService.GetDiseaseRiskForLocation(geonameId, user.DiseaseRelevanceSetting);
            }
            else
            {
                result = await _diseaseRiskService.GetDiseaseRiskForLocation(geonameId);
            }

            return Ok(result);
        }
    }
}