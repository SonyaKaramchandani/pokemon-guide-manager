using System.Threading.Tasks;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.HealthCareWorker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/healthcareworker")]
    public class HealthCareWorkerController : ControllerBase
    {
        private readonly ILogger<HealthCareWorkerController> _logger;
        private readonly IHealthCareWorkerService _healthCareWorkerService;

        public HealthCareWorkerController(ILogger<HealthCareWorkerController> logger,
            IHealthCareWorkerService healthCareWorkerService)
        {
            _logger = logger;
            _healthCareWorkerService = healthCareWorkerService;
        }

        [HttpGet("case")]
        public async Task<IActionResult> GetCaseList()
        {
            var result = await _healthCareWorkerService.GetCaseList();
            return Ok(result);
        }

        [HttpGet("case/{caseId}")]
        public async Task<IActionResult> GetCase(int caseId)
        {
            var result = await _healthCareWorkerService.GetCase(caseId);
            return Ok(result);
        }

        [HttpPost("case")]
        public async Task<IActionResult> CreateCase([FromBody] CreateCaseModel createCaseModel)
        {
            var result = await _healthCareWorkerService.CreateCase(createCaseModel);
            return Ok(result);
        }

        [HttpPut("case")]
        public async Task<IActionResult> UpdateCase([FromBody] UpdateCaseModel updateCaseModel)
        {
            var result = await _healthCareWorkerService.UpdateCase(updateCaseModel);
            return Ok(result);
        }

        [HttpPut("case/{caseId}/refine-by-symptoms")]
        public async Task<IActionResult> RefineCaseBySymptoms([FromBody] RefineCaseBySymptomsModel caseModel)
        {
            var result = await _healthCareWorkerService.RefineCaseBySymptoms(caseModel);
            return Ok(result);
        }

        [HttpPut("case/{caseId}/refine-by-activities-vaccines")]
        public async Task<IActionResult> RefineCaseByActivitiesAndVaccines([FromBody] RefineCaseByActivitiesAndVaccinesModel caseModel)
        {
            var result = await _healthCareWorkerService.RefineCaseByActivitiesAndVaccines(caseModel);
            return Ok(result);
        }
    }
}