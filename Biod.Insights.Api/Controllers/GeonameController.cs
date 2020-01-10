using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [Route("/api/geonamesearch")]
        [HttpGet]
        public async Task<IActionResult> SearchGeonames([Required] [FromQuery(Name = "name")] string searchTerm)
        {
            var result = await _geonameService.SearchGeonamesByTerm(searchTerm);
            return Ok(result);
        }
        
        [HttpGet("{geonameId}")]
        public async Task<IActionResult> GetGeonameShapes(int geonameId)
        {
            var result = await _geonameService.GetGeoname(geonameId, true);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeonameShapes([Required] [FromQuery(Name = "geonameIds")] string geonameIds)
        {
            var result = await _geonameService.GetGeonames(geonameIds.Split(',').Select(g => Convert.ToInt32(g)), true);
            return Ok(result);
        }
    }
}