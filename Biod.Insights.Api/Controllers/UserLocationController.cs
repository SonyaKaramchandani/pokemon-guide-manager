using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/user")]
    public class UserLocationController : ControllerBase
    {
        private readonly ILogger<UserLocationController> _logger;
        private readonly IUserLocationService _userLocationService;

        public UserLocationController(ILogger<UserLocationController> logger, IUserLocationService userLocationService)
        {
            _logger = logger;
            _userLocationService = userLocationService; //For more complex controllers use another business/domain layer
        }

        [HttpGet("{userId}/location")]
        public async Task<IActionResult> GetAoi([Required] string userId)
        {
            var result = await _userLocationService.GetAoi(userId);
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }

        [HttpPost("{userId}/location")]
        public async Task<IActionResult> AddAoi([Required] string userId, [FromBody] PostUserLocationModel userLocationModel)
        {
            var result = await _userLocationService.AddAoi(userId, userLocationModel.GeonameId.GetValueOrDefault());
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }

        [HttpDelete("{userId}/location/{geonameId}")]
        public async Task<IActionResult> DeleteAoi([Required] string userId, [Required] int geonameId)
        {
            var result = await _userLocationService.RemoveAoi(userId, geonameId);
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }
    }
}