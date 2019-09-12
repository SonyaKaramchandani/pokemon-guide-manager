using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Biod.Zebra
{
    public class MvcApplication : HttpApplication
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

        protected void Application_BeginRequest()
        {
            logger.SetLogicalThreadProperty("hostAddr", Request.UserHostAddress);
            logger.SetLogicalThreadProperty("browser", Request.Browser?.Type);
            logger.SetLogicalThreadProperty("url", Request.Url?.AbsoluteUri);
        }

        protected void Application_Error()
        {
            try
            {
                Exception exception = Server.GetLastError();
                logger.Error(exception.Message, exception);
                //Response.Clear();
                //Server.ClearError();
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception occurred at global application level", ex);
                throw ex;
            }
        }
    }
}
