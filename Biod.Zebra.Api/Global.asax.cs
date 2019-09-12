using Biod.Zebra.Library.Infrastructures.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Biod.Zebra.Api
{
    public class MvcApplication : System.Web.HttpApplication
    {
        readonly ILogger logger = Logger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
