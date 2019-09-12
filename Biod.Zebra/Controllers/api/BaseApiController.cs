using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures.Authentication;
using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Biod.Zebra.Controllers.api
{
    public abstract class BaseApiController : ApiController, IAuthenticatedApiController
    {
        protected ILogger Logger { get; }

        public BiodZebraEntities DbContext { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }

        protected string CurrentUserId { get; private set; }
        protected string CurrentUserName { get; private set; }

        public BaseApiController()
        {
            // Single instance of db context for a controller
            DbContext = new BiodZebraEntities();
            DbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
            Logger = Library.Infrastructures.Log.Logger.GetLogger(GetType().ToString());

            HttpRequest currentRequest = HttpContext.Current?.Request;
            Logger.SetLogicalThreadProperty("hostAddr", currentRequest?.UserHostAddress);
            Logger.SetLogicalThreadProperty("browser", currentRequest?.Browser?.Type);
            Logger.SetLogicalThreadProperty("url", currentRequest?.Url?.AbsoluteUri);

            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
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

        protected string GetModelStateErrors(ModelStateDictionary modelState)
        {
            var errors = modelState
                .SelectMany(x => x.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return string.Join("; ", errors);
        }
    }
}
