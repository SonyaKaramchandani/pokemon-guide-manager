using Biod.Zebra.Library.Infrastructures.Log;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Biod.Zebra.Library.Infrastructures.Authentication
{
    public class TokenAuthenticationAttribute : ActionFilterAttribute
    {
        public bool AllowNoUser = false;
        public string Roles;
        private ILogger _logger;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _logger = Logger.GetLogger(GetType().ToString());
            base.OnActionExecuting(actionContext);

            string token = GetHeader(actionContext.Request, Constants.LoginHeader.TOKEN_AUTHORIZATION);
            bool validToken = false;

            if (token != null)
            {
                validToken = LoginApiToken.ValidateToken(token, AllowNoUser, out var claims);
                if (validToken)
                {
                    // TODO: Verify claims, for now no roles validation

                    // Set the user details to the controller if applicable
                    IAuthenticatedApiController apiController = actionContext.ControllerContext.Controller as IAuthenticatedApiController;
                    apiController.SetCurrentUserId(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                    apiController.SetCurrentUserName(claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

                    return;
                }
            }

            // Failed Authentication, create Response
            HttpResponseMessage response;
            string error = "";
            if (validToken)
            {
                response = new HttpResponseMessage() { StatusCode = HttpStatusCode.Forbidden };
            }
            else
            {
                error = "Invalid token: Token Authentication failed";
                _logger.Warning($"{actionContext.Request.Method} call rejected due to reason: {error}");
                response = new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized };
            }
            response.Content = new StringContent(error, Encoding.UTF8, "text/plain");

            actionContext.Response = response;
        }

        private string GetHeader(HttpRequestMessage request, string key)
        {
            if (!request.Headers.TryGetValues(key, out var keys))
            {
                return null;
            }

            return keys.First();
        }
    }
}
