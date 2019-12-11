using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biod.Zebra.Controllers
{
    public class BaseController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        protected ILogger Logger { get; }
        protected string UserName { get; private set; }

        public BiodZebraEntities DbContext { get; set; }

        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return _userManager
                    ?? HttpContext?.GetOwinContext()?.GetUserManager<ApplicationUserManager>()
                    ?? new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            }
            protected set
            {
                _userManager = value;
            }
        }


        public BaseController()
        {
            // Single instance of db context for a controller
            DbContext = new BiodZebraEntities();
            DbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
            Logger = Library.Infrastructures.Log.Logger.GetLogger(GetType().ToString());
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            UserName = User?.Identity?.Name ?? "(unknown user)";

            return base.BeginExecuteCore(callback, state);
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