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
    [Route("api/geoname")]
    public class GeonameController : ControllerBase
    {
        private readonly ILogger<GeonameController> _logger;
        private readonly IGeonameService _geonameService;

        public GeonameController(ILogger<GeonameController> logger, IGeonameService geonameService)
        {
            _logger = logger;
            _geonameService = geonameService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchGeonames([Required] [FromQuery(Name = "name")] string searchTerm)
        {
            var result = await _geonameService.SearchGeonamesByTerm(searchTerm);
            return Ok(result);
        }
    }
}