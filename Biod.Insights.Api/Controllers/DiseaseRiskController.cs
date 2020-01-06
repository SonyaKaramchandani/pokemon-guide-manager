using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/diseaserisk")]
    public class DiseaseRiskController : ControllerBase
    {
        private readonly ILogger<DiseaseRiskController> _logger;
        private readonly IDiseaseRiskService _diseaseRiskService;

        public DiseaseRiskController(ILogger<DiseaseRiskController> logger, IDiseaseRiskService diseaseRiskService)
        {
            _logger = logger;
            _diseaseRiskService = diseaseRiskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRiskForLocation([FromQuery] int? geonameId = null)
        {
            var result = await _diseaseRiskService.GetDiseaseRiskForLocation(geonameId);
            return Ok(result);
        }
    }
}