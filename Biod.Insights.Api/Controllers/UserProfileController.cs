using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Common;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Account;
using Biod.Insights.Service.Models.User;
using Biod.Insights.Common.Exceptions;
using Biod.Insights.Service.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/userprofile")]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly IEnvironmentVariables _environmentVariables;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;
        private readonly IUserService _userService;

        public UserProfileController(
            ILogger<UserProfileController> logger,
            IEnvironmentVariables environmentVariables,
            IDiseaseRelevanceService diseaseRelevanceService,
            IUserService userService)
        {
            _logger = logger;
            _environmentVariables = environmentVariables;
            _diseaseRelevanceService = diseaseRelevanceService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUser()
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }
            
            var result = await _userService.GetUser(new UserConfig.Builder().SetUserId(tokenUserId).Build());
            return Ok(result);
        }

        [HttpPut("details")]
        public async Task<ActionResult<UserModel>> UpdatePersonalDetails([Required] [FromBody] UserPersonalDetailsModel personalDetailsModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var config = new UserConfig.Builder()
                .SetUserId(tokenUserId)
                .ShouldTrackChanges()
                .Build();
            var result = await _userService.UpdatePersonalDetails(config, personalDetailsModel);
            return Ok(result);
        }

        [HttpPut("notifications")]
        public async Task<ActionResult<UserModel>> UpdateNotificationSettings([Required] [FromBody] UserNotificationsModel notificationsModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var config = new UserConfig.Builder()
                .SetUserId(tokenUserId)
                .ShouldTrackChanges()
                .Build();
            var result = await _userService.UpdateNotificationSettings(config, notificationsModel);
            return Ok(result);
        }

        [HttpPut("customsettings")]
        public async Task<ActionResult<UserModel>> UpdateCustomSettings([Required] [FromBody] UserCustomSettingsModel customSettingsModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var config = new UserConfig.Builder()
                .SetUserId(tokenUserId)
                .ShouldTrackChanges()
                .Build();
            var result = await _userService.UpdateCustomSettings(config, customSettingsModel);
            return Ok(result);
        }

        [HttpPut("password")]
        public async Task<ActionResult<UserModel>> UpdatePassword([Required] [FromBody] ChangePasswordModel changePasswordModel)
        {
            var tokenUserId = ClaimsHelper.GetUserId(HttpContext.User?.Claims, _environmentVariables);
            if (string.IsNullOrWhiteSpace(tokenUserId))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden, "User does not have permission to perform this operation");
            }

            var config = new UserConfig.Builder()
                .SetUserId(tokenUserId)
                .ShouldTrackChanges()
                .Build();
            await _userService.UpdatePassword(config, changePasswordModel);
            return Ok();
        }
    }
}