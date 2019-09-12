using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Diagnostics;
using System.IO;


namespace BlueDot.DiseasesAPI
{
    /// <summary>
    /// WebApi Config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Attribute routing.
            config.MapHttpAttributeRoutes();

            // Convention-based routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Filters.Add(new AuthorizeAttribute());  // Only authenticated users can access the API

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            System.Web.Http.Tracing.SystemDiagnosticsTraceWriter traceWriter = config.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;                       // TODO:  makes messages include more info
            traceWriter.MinimumLevel = System.Web.Http.Tracing.TraceLevel.Warn;        // TODO:  levels include Debug, Info, Warn, Error, Fatal

            // Note:  the directory used here should have been already created by hand...
            String logfile = ((DateTime.Now.Ticks - Globals.referenceDatum.Ticks) / 10000000).ToString();
            //Trace.Listeners.Add(new TextWriterTraceListener(File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APILogs", logfile + ".txt"))));
        }
    }
}
