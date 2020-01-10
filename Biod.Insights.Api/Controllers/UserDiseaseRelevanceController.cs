using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/user")]
    public class UserDiseaseRelevanceController : ControllerBase
    {
        private readonly ILogger<UserLocationController> _logger;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;

        public UserDiseaseRelevanceController(ILogger<UserLocationController> logger, IDiseaseRelevanceService diseaseRelevanceService)
        {
            _logger = logger;
            _diseaseRelevanceService = diseaseRelevanceService; //For more complex controllers use another business/domain layer
        }
        
        [HttpGet("{userId}/diseaserelevance")]
        public async Task<IActionResult> GetAoi([Required] string userId)
        {
            var result = await _diseaseRelevanceService.GetUserDiseaseRelevanceSettings(userId);
            return Ok(result);
        }
    }
}