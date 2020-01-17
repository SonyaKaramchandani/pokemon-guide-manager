using System.Threading.Tasks;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Disease;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/diseaserisk")]
    public class DiseaseRiskController : ControllerBase
    {
        private readonly ILogger<DiseaseRiskController> _logger;
        private readonly IDiseaseRiskService _diseaseRiskService;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;
        private readonly IUserService _userService;

        public DiseaseRiskController(
            ILogger<DiseaseRiskController> logger,
            IDiseaseRiskService diseaseRiskService,
            IDiseaseRelevanceService diseaseRelevanceService,
            IUserService userService)
        {
            _logger = logger;
            _diseaseRiskService = diseaseRiskService;
            _diseaseRelevanceService = diseaseRelevanceService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRiskForLocation([FromQuery] int? geonameId = null)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            RiskAggregationModel result;
            if (!string.IsNullOrWhiteSpace(tokenUserId))
            {
                var user = await _userService.GetUser(tokenUserId);
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