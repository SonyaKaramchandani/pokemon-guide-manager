using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    public class UserLocationController : ControllerBase
    {
        private readonly ILogger<UserLocationController> _logger;
        private readonly IUserLocationService _userLocationService;

        public UserLocationController(ILogger<UserLocationController> logger, IUserLocationService userLocationService)
        {
            _logger = logger;
            _userLocationService = userLocationService; //For more complex controllers use another business/domain layer
        }

        [HttpGet]
        [Route("api/user/{userId}/location")]
        public async Task<IActionResult> GetAoi([Required] string userId)
        {
            var result = await _userLocationService.GetAoi(userId);
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }

        [HttpPut]
        [Route("api/user/{userId}/location")]
        public async Task<IActionResult> GetAoi([Required] string userId, [FromBody] PutUserLocationModel userLocationModel)
        {
            var result = await _userLocationService.AddAoi(userId, userLocationModel.GeonameId.GetValueOrDefault());
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }

        [HttpDelete]
        [Route("api/user/{userId}/location/{geonameId}")]
        public async Task<IActionResult> GetAoi([Required] string userId, [Required] int geonameId)
        {
            var result = await _userLocationService.RemoveAoi(userId, geonameId);
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }
    }
}