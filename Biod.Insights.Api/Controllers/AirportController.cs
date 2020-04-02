using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/airport")]
    public class AirportController : ControllerBase
    {
        private readonly ILogger<AirportController> _logger;
        private readonly IAirportService _airportService;

        public AirportController(ILogger<AirportController> logger, IAirportService airportService)
        {
            _logger = logger;
            _airportService = airportService;
        }

        [HttpGet("destination")]
        public async Task<ActionResult> GetDestinationAirports([Required] [FromQuery] int eventId, [FromQuery] int? geonameId)
        {
            var airportConfigBuilder = new AirportConfig.Builder(eventId);
            var result = await _airportService.GetDestinationAirports(
                airportConfigBuilder
                    .ShouldIncludeImportationRisk(geonameId)
                    .Build());
            return Ok(result);
        }
    }
}