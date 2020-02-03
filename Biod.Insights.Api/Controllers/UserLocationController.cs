using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/userlocation")]
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
        public async Task<IActionResult> GetAoi()
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var result = await _userLocationService.GetAoi(tokenUserId);
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddAoi([FromBody] PostUserLocationModel userLocationModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var result = await _userLocationService.AddAoi(tokenUserId, userLocationModel.GeonameId.GetValueOrDefault());
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }

        [HttpDelete("{geonameId}")]
        public async Task<IActionResult> DeleteAoi([Required] int geonameId)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var result = await _userLocationService.RemoveAoi(tokenUserId, geonameId);
            return Ok(new GetUserLocationModel
            {
                Geonames = result
            });
        }
    }
}