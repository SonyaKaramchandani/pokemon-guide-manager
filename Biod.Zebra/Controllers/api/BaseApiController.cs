using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures.Authentication;
using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Biod.Zebra.Library.Infrastructures.Notification;
using Microsoft.AspNet.Identity.Owin;

namespace Biod.Zebra.Controllers.api
{
    public abstract class BaseApiController : ApiController, IAuthenticatedApiController
    {
        private UserManager<ApplicationUser> _userManager;
        
        protected ILogger Logger { get; }

        public INotificationDependencyFactory NotificationDependencyFactory = new NotificationDependencyFactory();
        public BiodZebraEntities DbContext { get; set; }
        public UserManager<ApplicationUser> UserManager
        {
            get =>
                _userManager
                ?? Request.GetOwinContext()?.GetUserManager<ApplicationUserManager>()
                ?? new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            protected set => _userManager = value;
        }

        protected string CurrentUserId { get; private set; }
        protected string CurrentUserName { get; private set; }

        public BaseApiController()
        {
            // Single instance of db context for a controller
            DbContext = new BiodZebraEntities();
            DbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
            Logger = Library.Infrastructures.Log.Logger.GetLogger(GetType().ToString());

            var currentRequest = HttpContext.Current?.Request;
            Logger.SetLogicalThreadProperty("hostAddr", currentRequest?.UserHostAddress);
            Logger.SetLogicalThreadProperty("browser", currentRequest?.Browser?.Type);
            Logger.SetLogicalThreadProperty("url", currentRequest?.Url.AbsoluteUri);
        }

        [NonAction]
        public void SetCurrentUserId(string userId)
        {
            CurrentUserId = userId;
        }

        [NonAction]
        public void SetCurrentUserName(string userName)
        {
            CurrentUserName = userName;
            Logger.SetLogicalThreadProperty("userName", CurrentUserName);
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

        protected static string GetModelStateErrors(ModelStateDictionary modelState)
        {
            var errors = modelState
                .SelectMany(x => x.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToArray();

            return string.Join(@"\n", errors);
        }

        protected static string GetIdentityResultErrors(IdentityResult identityResult)
        {
            var errors = identityResult.Errors
                .Where(error => !(error.StartsWith("Name") && error.EndsWith("is already taken.")) && !error.StartsWith("The GeonameId"))
                .ToList();

            return string.Join("\n", errors);
        }
    }
}
