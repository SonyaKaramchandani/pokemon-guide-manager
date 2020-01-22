using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Geoname;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> GetGeonameShapes([FromBody] GetGeonameShapesModel model)
        {
            if (model?.GeonameIds == null || !model.GeonameIds.Any())
            {
                return Ok(new List<GetGeonameModel>());
            }
            
            var result = await _geonameService.GetGeonames(model.GeonameIds, true);
            return Ok(result);
        }
    }
}