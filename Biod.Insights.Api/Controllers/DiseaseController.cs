using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/disease")]
    public class DiseaseController : ControllerBase
    {
        private readonly ILogger<DiseaseController> _logger;
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(ILogger<DiseaseController> logger, IDiseaseService diseaseService)
        {
            _logger = logger;
            _diseaseService = diseaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiseases()
        {
            var diseaseConfig = new DiseaseConfig.Builder().ShouldIncludeAllProperties().Build();
            var result = await _diseaseService.GetDiseases(diseaseConfig);
            return Ok(result);
        }

        [HttpGet("{diseaseId}")]
        public async Task<IActionResult> GetDisease([Required] int diseaseId)
        {
            var diseaseConfig = new DiseaseConfig.Builder()
                .ShouldIncludeAllProperties()
                .AddDiseaseId(diseaseId)
                .Build();
            var result = await _diseaseService.GetDisease(diseaseConfig);
            return Ok(result);
        }

        [HttpGet("{diseaseId}/casecount")]
        public async Task<IActionResult> GetDiseaseCaseCount([Required] int diseaseId, [FromQuery] int? geonameId)
        {
            var result = await _diseaseService.GetDiseaseCaseCount(diseaseId, geonameId);
            return Ok(result);
        }
    }
}