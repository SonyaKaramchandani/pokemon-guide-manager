using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Service.Models.Event;
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
        public async Task<ActionResult<IEnumerable<DiseaseInformationModel>>> GetDiseases()
        {
            var diseaseConfig = new DiseaseConfig.Builder().ShouldIncludeAllProperties().Build();
            var result = await _diseaseService.GetDiseases(diseaseConfig);
            return Ok(result);
        }

        [HttpGet("{diseaseId}")]
        public async Task<ActionResult<DiseaseInformationModel>> GetDisease([Required] int diseaseId)
        {
            var diseaseConfig = new DiseaseConfig.Builder()
                .ShouldIncludeAllProperties()
                .AddDiseaseId(diseaseId)
                .Build();
            var result = await _diseaseService.GetDisease(diseaseConfig);
            return Ok(result);
        }

        [HttpGet("{diseaseId}/casecount")]
        public async Task<ActionResult<IEnumerable<ProximalCaseCountModel>>> GetDiseaseCaseCount([Required] int diseaseId, [FromQuery] int? geonameId)
        {
            var result = await _diseaseService.GetDiseaseCaseCount(diseaseId, geonameId);
            return Ok(result);
        }

        /// <summary>
        /// Gets the diseases grouped for usage in the Disease Relevance Matrix.
        /// </summary>
        [HttpGet("groups")]
        public async Task<ActionResult<IEnumerable<DiseaseGroupModel>>> GetDiseaseGroups()
        {
            var result = await _diseaseService.GetDiseaseGroups();
            return Ok(result);
        }
    }
}