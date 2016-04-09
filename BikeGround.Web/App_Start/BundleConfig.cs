using System.Web;
using System.Web.Optimization;

namespace BikeGround.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.signalR-2.2.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/font-awesome-4.3.0/css/font-awesome.min.css",
                      "~/Content/angular-ui-notification.min.css",
                      "~/Content/angular-toggle-switch-bootstrap.css",
                      "~/Content/ngProgress.css",
                      "~/Content/timeline.css",
                      "~/Content/style.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-resource.min.js",
                "~/Scripts/textAngular-sanitize.min.js",
                "~/Scripts/angular-ui-notification.min.js",
                "~/Scripts/angular-animate.min.js",
                "~/Scripts/angular-toggle-switch.min.js",
                "~/Scripts/textAngular.min.js",
                "~/Scripts/ngProgress.min.js",
                //"~/Scripts/flot-data.js",
                "~/Scripts/raphael-min.js",
                "~/Scripts/morris.min.js",
                "~/Scripts/morris-data.js",
                "~/App/BikeGround.js",
                "~/App/Services/ObjectFactory.js",
                "~/App/Services/GlobalFactory.js",
                "~/App/Services/InitiatorFactory.js",
                //"~/App/Services/ProfileFactory.js",
                //"~/App/Services/BlogFactory.js",
                //"~/App/Services/PostFactory.js",
                //"~/App/Services/WallFactory.js",
                "~/App/GlobalModule.js",
                "~/App/ConnectModule.js",
                "~/App/ProfileModule.js",
                "~/App/BlogModule.js",
                "~/App/PostModule.js",
                "~/App/WallModule.js",
                "~/App/TripModule.js",
                "~/App/MailboxModule.js",
                "~/App/CommentModule.js",
                "~/App/SettingsModule.js",
                "~/App/AnalyticsModule.js",
                "~/Scripts/sb-admin-2.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular_en-us").Include(
                "~/Scripts/i18n/angular-locale_en-us.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular_en-gb").Include(
                "~/Scripts/i18n/angular-locale_en-gb.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular_hr-hr").Include(
                "~/Scripts/i18n/angular-locale_hr-hr.js"
            ));
        }
    }
}
