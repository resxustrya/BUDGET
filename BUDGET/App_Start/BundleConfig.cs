using System.Web;
using System.Web.Optimization;

namespace BUDGET
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/page/scripts.js",
                      "~/Scripts/toggle.js"));


            bundles.Add(new StyleBundle("~/login/css").Include(
                      "~/Content/bootstrap.css", "~/Content/sb-admin2.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sb-admin2.css",
                      "~/Content/handson/pikaday.css",
                      "~/Content/handson/handsontable.css",
                      "~/Content/scrollstyle.css",
                      "~/Content/Site.css",
                      "~/Content/lobibox/lobibox.min.css"
                      ));

            

            bundles.Add(new StyleBundle("~/font-awesome").Include("~/Content/vendor/font-awesome/font-awesome.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/sb-admin-js").Include(
                      "~/Content/handson/moment.js",
                      "~/Content/handson/pikaday.js",
                      "~/Content/handson/languages.js",
                      "~/Content/handson/handsontable.js",
                      "~/Scripts/lobibox/lobibox.min.js"));

        }
    }
}
