using System.Web;
using System.Web.Optimization;

namespace Biod.Surveillance
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-1.12.4.min.js",
                         "~/Scripts/jquery-ui.min.js",
                         "~/Scripts/jquery.dialogextend.js",
                         "~/Scripts/loading.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/jquery.signalR-2.2.2.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      //"~/Scripts/bootstrap-4.0.0.bundle.min.js",
                      "~/Scripts/datatables.min.js",
                      "~/Scripts/chosen.jquery.min-v1.8.3.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
            //          "~/Scripts/index.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.min.css",
                       //"~/Content/bootstrap-4.0.0.min.css",
                      "~/Content/datatables.min.css",
                      "~/Content/chosen.min-v1.8.3.css",
                       "~/Content/jquery-ui.css",
                      "~/Content/site.css"));
        }
    }
}
