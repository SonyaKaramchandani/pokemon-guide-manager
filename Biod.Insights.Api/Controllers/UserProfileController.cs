using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Account;
using Biod.Insights.Api.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/userprofile")]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;
        private readonly IUserService _userService;

        public UserProfileController(
            ILogger<UserProfileController> logger,
            IDiseaseRelevanceService diseaseRelevanceService,
            IUserService userService)
        {
            _logger = logger;
            _diseaseRelevanceService = diseaseRelevanceService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUser()
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var result = await _userService.GetUser(tokenUserId);
            return Ok(result);
        }

        [HttpPut("details")]
        public async Task<ActionResult<UserModel>> UpdatePersonalDetails([Required] [FromBody] UserPersonalDetailsModel personalDetailsModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var result = await _userService.UpdatePersonalDetails(tokenUserId, personalDetailsModel);
            return Ok(result);
        }

        [HttpPut("notifications")]
        public async Task<ActionResult<UserModel>> UpdateNotificationSettings([Required] [FromBody] UserNotificationsModel notificationsModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var result = await _userService.UpdateNotificationSettings(tokenUserId, notificationsModel);
            return Ok(result);
        }

        [HttpPut("customsettings")]
        public async Task<ActionResult<UserModel>> UpdateCustomSettings([Required] [FromBody] UserCustomSettingsModel customSettingsModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var result = await _userService.UpdateCustomSettings(tokenUserId, customSettingsModel);
            return Ok(result);
        }

        [HttpPut("password")]
        public async Task<ActionResult<UserModel>> UpdatePassword([Required] [FromBody] ChangePasswordModel changePasswordModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            await _userService.UpdatePassword(tokenUserId, changePasswordModel);
            return Ok();
        }
    }
}