using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.ModelBinding;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.Authentication;
using Biod.Zebra.Library.Infrastructures.Notification;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Notification.Email;
using Biod.Zebra.Library.Models.User;
using Microsoft.AspNet.Identity;
using Constants = Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Zebra.Controllers.api
{
    // Prefixing this with /mvcapi for APIs that use MVC authorization instead of Token Authorization
    [Authorize]
    [RoutePrefix("mvcapi/user")]
    public class UserController : BaseApiController
    {
        [Authorize(Roles = "Admin")]
        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> Create(CreateUserViewModel model)
        {
            try
            {
                // Validation
                if (!ModelState.IsValid)
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(GetModelStateErrors(ModelState), System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                if (!model.ResetPasswordRequired && string.IsNullOrWhiteSpace(model.Password))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("The Password field is required if not forcing the user to reset their password in the email confirmation.", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                var customRolesFilter = new CustomRolesFilter(DbContext);
                var publicRole = customRolesFilter.GetFirstPublicRole(model.RoleNames);
                if (publicRole == null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent($"At least 1 public role is required. Select from: {string.Join(", ", customRolesFilter.GetPublicRoleNames())}", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                // Validation passed
                var result = CreateUser(model, out var user);
                if (!result.Succeeded)
                {
                    var errors = GetIdentityResultErrors(result);
                    Logger.Warning($"Failed to create new user: {errors}");
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(errors, System.Text.Encoding.UTF8, "text/html");
                    return response;
                }
                UserManager.AddToRoles(user.Id, model.RoleNames);
                await Task.WhenAll(
                    SendRegistrationEmail(user, model.ResetPasswordRequired),
                    AccountHelper.PrecalculateRisk(user.Id)
                );
                
                Logger.Info($"New user with ID {user.Id} has been successfully registered");
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unexpected error occurred while creating user: {ex.Message}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("SendRegistrationEmail")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendRegistrationEmail([QueryString] string userId)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(userId))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("The userId parameter is required", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                // Validation passed
                var user = await UserManager.FindByIdAsync(userId);
                if (user == null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.Content = new StringContent("The user does not exist", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                await SendRegistrationEmail(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unexpected error occurred while sending registration email to user {userId}: {ex.Message}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [Route("SendWelcomeEmail")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendWelcomeEmail([QueryString] string userId, [QueryString] string code)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("The userId and code parameters are required", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }
                
                if (!UserManager.VerifyUserToken(userId, Constants.IdentityTokenPurpose.EMAIL_CONFIRMATION, code))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("Invalid user or code", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                // Validation Passed
                var user = await UserManager.FindByIdAsync(userId);
                await new NotificationHelper(DbContext, UserManager).SendZebraNotification(new WelcomeEmailViewModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    AoiGeonameIds = user.AoiGeonameIds,
                    Title = ConfigurationManager.AppSettings.Get("ZebraWelcomeEmailSubject"),
                    UserLocationName = user.Location
                }.Compute(DbContext));
                
                Logger.Info($"Successfully sent Welcome Email to user {userId}");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unexpected error occurred while sending registration email to user {userId}: {ex.Message}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("CompleteRegistration")]
        [HttpPost]
        public async Task<HttpResponseMessage> CompleteRegistration([QueryString] string userId, [QueryString] string code, [FromBody] ResetPasswordViewModel model)
        {
            try
            {
                // Validation
                if (model == null || !ModelState.IsValid)
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(GetModelStateErrors(ModelState), System.Text.Encoding.UTF8, "text/html");
                    return response;
                }
                
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("The userId and code parameters are required", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }
                
                // In completing registration workflow, the user came from a confirmation email link, so the token will be an email confirmation token
                if (!UserManager.VerifyUserToken(userId, Constants.IdentityTokenPurpose.EMAIL_CONFIRMATION, code))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("Invalid user or code", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                // Validation passed
                var result = await ResetPassword(userId, UserManager.GeneratePasswordResetToken(userId), model.Password);
                if (!result.Succeeded)
                {
                    Logger.Warning($"Failed to set new password for user {userId} during completion of registration: {GetIdentityResultErrors(result)}");
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("Failed to set new password. If the problem persists please contact support.", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }
                
                Logger.Info($"Successfully set new password for user {userId} during completion of registration");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unexpected error occurred while setting new password during completion of registration for user {userId}: {ex.Message}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ResetPassword")]
        [HttpPost]
        public async Task<HttpResponseMessage> ResetPassword([QueryString] string userId, [QueryString] string code, [FromBody] ResetPasswordViewModel model)
        {
            try
            {
                // Validation
                if (!ModelState.IsValid)
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(GetModelStateErrors(ModelState), System.Text.Encoding.UTF8, "text/html");
                    return response;
                }
                
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("The userId and code parameters are required", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }

                // Validation passed
                var result = await ResetPassword(userId, code, model.Password);
                if (!result.Succeeded)
                {
                    Logger.Warning($"Failed to set new password for user {userId} during reset password: {GetIdentityResultErrors(result)}");
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("Failed to set new password. If the problem persists please contact support.", System.Text.Encoding.UTF8, "text/html");
                    return response;
                }
                
                Logger.Info($"Successfully set new password for user {userId} during reset password");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.Error($"Unexpected error occurred while setting new password during reset password for user {userId}: {ex.Message}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        private IdentityResult CreateUser(CreateUserViewModel model, out ApplicationUser user)
        {
            var password = model.Password ?? Guid.NewGuid().ToString();
            var gridId = DbContext.usp_ZebraPlaceGetGridIdByGeonameId(model.LocationGeonameId).FirstOrDefault();
            var locationName = DbContext.usp_ZebraPlaceGetLocationNameByGeonameId(model.LocationGeonameId).FirstOrDefault();

            if (gridId == null || locationName == null)
            {
                Logger.Warning($"Geoname Id '{model.LocationGeonameId}' lookup resulted in Grid Id '{gridId}' and Location Name '{locationName}'.");
            }

            user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Location = locationName,
                GeonameId = model.LocationGeonameId,
                GridId = gridId,
                Organization = model.Organization,
                PhoneNumber = model.PhoneNumber,
                NewCaseNotificationEnabled = true,
                NewOutbreakNotificationEnabled = true,
                PeriodicNotificationEnabled = true,
                WeeklyOutbreakNotificationEnabled = true,
                SmsNotificationEnabled = false,
                AoiGeonameIds = model.LocationGeonameId.ToString()
            };

            return UserManager.Create(user, password);
        }

        private async Task<IdentityResult> ResetPassword(string userId, string code, string password)
        {
            return await UserManager.ResetPasswordAsync(userId, code, password);
        }

        private async Task SendRegistrationEmail(ApplicationUser user, bool resetPasswordRequired = true)
        {
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Link("Default", new
            {
                Controller = "Account", 
                Action = resetPasswordRequired ? "CompleteRegistration" : "ConfirmEmail",
                userId = user.Id,
                utm_source = Constants.GoogleAnalytics.UrlTracking.UTM_SOURCE_EMAIL,
                utm_medium = Constants.GoogleAnalytics.UrlTracking.UTM_MEDIUM_EMAIL,
                utm_campaign = Constants.GoogleAnalytics.UrlTracking.UTM_CAMPAIGN_CONFIRMATION,
                code
            });

            var model = resetPasswordRequired ? new RegistrationEmailViewModel() : new ConfirmationEmailViewModel();

            model.UserId = user.Id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.DoNotTrackEnabled = user.DoNotTrackEnabled;
            model.EmailConfirmed = user.EmailConfirmed;
            model.CallbackUrl = callbackUrl;
            model.AoiGeonameIds = user.AoiGeonameIds;
            model.Title = ConfigurationManager.AppSettings.Get("ZebraConfirmationEmailSubject");

            await new NotificationHelper(DbContext, UserManager).SendZebraNotification(model);
        }
    }
}