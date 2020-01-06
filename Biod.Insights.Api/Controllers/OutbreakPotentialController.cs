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
    [Route("api/outbreakpotential")]
    public class OutbreakPotentialController : ControllerBase
    {
        private readonly ILogger<OutbreakPotentialController> _logger;
        private readonly IOutbreakPotentialService _outbreakPotentialService;

        public OutbreakPotentialController(ILogger<OutbreakPotentialController> logger, IOutbreakPotentialService outbreakPotentialService)
        {
            _logger = logger;
            _outbreakPotentialService = outbreakPotentialService;
        }

        [HttpGet("{geonameId}")]
        public async Task<IActionResult> GetOutbreakPotentials([Required] int geonameId)
        {
            var result = await _outbreakPotentialService.GetOutbreakPotentialByGeonameId(geonameId);
            return Ok(result);
        }
    }
}