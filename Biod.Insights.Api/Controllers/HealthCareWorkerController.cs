using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.HealthCareWorker;
using Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels;
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
        public async Task<ActionResult<IEnumerable<GetCaseModel>>> GetCaseList()
        {
            var caseList = await _healthCareWorkerService.GetCaseList();
            return Ok(caseList);
        }

        [HttpGet("case/{caseId}")]
        public async Task<ActionResult<GetCaseModel>> GetCase(int caseId)
        {
            var caseModel = await _healthCareWorkerService.GetCase(caseId);
            return Ok(caseModel);
        }

        [HttpPost("case")]
        public async Task<ActionResult<int>> CreateCase([FromBody] CreateCaseModel createCaseModel)
        {
            var caseId = await _healthCareWorkerService.CreateCase(createCaseModel);
            return Ok(caseId);
        }

        [HttpPut("case")]
        public async Task<OkResult> UpdateCase([FromBody] UpdateCaseModel updateCaseModel)
        {
            await _healthCareWorkerService.UpdateCase(updateCaseModel);
            return Ok();
        }

        [HttpPut("case/{caseId}/refine-by-symptoms")]
        public async Task<ActionResult<IEnumerable<RefinementSymptomsHealthCareWorkerModel>>> RefineCaseBySymptoms([FromBody] RefineCaseBySymptomsModel caseModel)
        {
            var diseasesBySymptoms = await _healthCareWorkerService.RefineCaseBySymptoms(caseModel);
            return Ok(diseasesBySymptoms);
        }

        [HttpPut("case/{caseId}/refine-by-activities-vaccines")]
        public async Task<ActionResult<IEnumerable<RefinementActivitiesAndVaccinesHealthCareWorkerModel>>> RefineCaseByActivitiesAndVaccines(
            [FromBody] RefineCaseByActivitiesAndVaccinesModel caseModel)
        {
            var diseasesByActivitiesAndVaccines = await _healthCareWorkerService.RefineCaseByActivitiesAndVaccines(caseModel);
            return Ok(diseasesByActivitiesAndVaccines);
        }
    }
}