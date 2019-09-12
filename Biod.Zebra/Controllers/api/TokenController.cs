using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.Authentication;
using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Biod.Zebra.Controllers.api
{
    public class TokenController : BaseApiController
    {
        [AllowAnonymous]
        [Route("api/token")]
        public HttpResponseMessage GetToken()
        {
            try
            {
                Request.Headers.TryGetValues(Library.Infrastructures.Constants.LoginHeader.USERNAME, out var username);
                Request.Headers.TryGetValues(Library.Infrastructures.Constants.LoginHeader.PASSWORD, out var password);
                Request.Headers.TryGetValues(Library.Infrastructures.Constants.LoginHeader.FIREBASE_DEVICE_ID, out var deviceToken);

                var usernameValue = username?.FirstOrDefault();
                var passwordValue = password?.FirstOrDefault();
                var deviceTokenValue = deviceToken?.FirstOrDefault();

                // Check that all required header fields were provided
                if (string.IsNullOrWhiteSpace(usernameValue) || string.IsNullOrWhiteSpace(passwordValue) || string.IsNullOrWhiteSpace(deviceTokenValue))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, 
                        $"Login request requires the '{Library.Infrastructures.Constants.LoginHeader.USERNAME}', '{Library.Infrastructures.Constants.LoginHeader.PASSWORD}', and '{Library.Infrastructures.Constants.LoginHeader.FIREBASE_DEVICE_ID}' fields in the request header");
                }

                // Check that the user credentials are valid
                var user = UserManager.FindByName(usernameValue);
                if (user == null || !UserManager.CheckPassword(user, passwordValue))
                {
                    Logger.Warning($"Failed login attempt for user with username {usernameValue} via the Login API");
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid user credentials.");
                }

                // TODO: Save device token to the user
                ExternalIdentifierHelper.RegisterExternalIdentifier(DbContext, Library.Infrastructures.Constants.ExternalIdentifiers.FIREBASE_FCM, deviceTokenValue, user.Id);

                string token = LoginApiToken.GenerateToken(user, deviceTokenValue);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(token, System.Text.Encoding.UTF8, "text/plain");

                Logger.Info($"Successfully returned login token to user id {user.Id}");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured for the login request", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            if (disposing && DbContext != null)
            {
                DbContext.Dispose();
                DbContext = null;
            }
            base.Dispose(disposing);
        }
    }
}
