using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Biod.Products.Common;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Geoname;
using Biod.Insights.Service.Models.User;
using Biod.Products.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/userlocation")]
    public class UserLocationController : ControllerBase
    {
        private readonly ILogger<UserLocationController> _logger;
        private readonly IEnvironmentVariables _environmentVariables;
        private readonly IUserLocationService _userLocationService;

        public UserLocationController(
            ILogger<UserLocationController> logger,
            IEnvironmentVariables environmentVariables,
            IUserLocationService userLocationService)
        {
            _logger = logger;
            _environmentVariables = environmentVariables;
            _userLocationService = userLocationService; //For more complex controllers use another business/domain layer
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetGeonameModel>>> GetAoi()
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
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
        public async Task<ActionResult<IEnumerable<GetGeonameModel>>> AddAoi([FromBody] PostUserLocationModel userLocationModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
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
        public async Task<ActionResult<IEnumerable<GetGeonameModel>>> DeleteAoi([Required] int geonameId)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
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