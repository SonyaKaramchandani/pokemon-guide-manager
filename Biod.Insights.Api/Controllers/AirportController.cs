using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [AllowAnonymous]
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
    }
}