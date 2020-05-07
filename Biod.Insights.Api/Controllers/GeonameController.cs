using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Geoname;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IEnumerable<SearchGeonameModel>>> SearchGeonames([Required] [FromQuery(Name = "name")] string searchTerm)
        {
            var result = await _geonameService.SearchGeonamesByTerm(searchTerm);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("/api/citysearch")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchGeonameModel>>> SearchCityGeonames([Required] [FromQuery(Name = "name")] string searchTerm)
        {
            var result = await _geonameService.SearchCitiesByTerm(searchTerm);
            return Ok(result);
        }

        [HttpGet("{geonameId}")]
        public async Task<ActionResult<GetGeonameModel>> GetGeonameShapes(int geonameId)
        {
            var config = new GeonameConfig.Builder()
                .ShouldIncludeShape()
                .AddGeonameId(geonameId)
                .Build();

            var result = await _geonameService.GetGeoname(config);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<GetGeonameModel>>> GetGeonameShapes([FromBody] GetGeonameShapesModel model)
        {
            if (model?.GeonameIds == null || !model.GeonameIds.Any())
            {
                return Ok(new List<GetGeonameModel>());
            }

            var config = new GeonameConfig.Builder()
                .ShouldIncludeShape()
                .AddGeonameIds(model.GeonameIds)
                .Build();

            var result = await _geonameService.GetGeonames(config);
            return Ok(result);
        }

        [HttpGet("{geonameId}/grid")]
        public ActionResult<IEnumerable<string>> GetGeonameGrids([Required] int geonameId)
        {
            var result = _geonameService.GetGridIdsByGeonameId(geonameId);
            return Ok(result);
        }
    }
}