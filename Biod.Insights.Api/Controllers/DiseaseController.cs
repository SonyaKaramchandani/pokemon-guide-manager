using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
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
        public async Task<IActionResult> GetDisease()
        {
            var result = await _diseaseService.GetDiseases();
            return Ok(result);
        }

        [HttpGet("{diseaseId}")]
        public async Task<IActionResult> GetDisease([Required] int diseaseId)
        {
            var result = await _diseaseService.GetDisease(diseaseId);
            return Ok(result);
        }
    }
}