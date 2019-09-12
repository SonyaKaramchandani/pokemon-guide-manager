using System.Configuration;
using System.Web;
using System.Web.Optimization;

namespace Biod.Zebra
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.min.js",
                         "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/zebra").Include(
            //    "~/Scripts/zebra.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/animate.css"));

            //bundles.Add(new StyleBundle("~/Content/globaldashboardcss").Include(
            //        "~/Areas/DashboardPage/Content/globaldashboard.min.css?version=5"));
        }
    }
}
