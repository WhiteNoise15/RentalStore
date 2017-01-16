using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RentalStore.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/vendors/jquery-1.10.2.min.js",
                "~/Scripts/vendors/bootstrap.min.js",
                "~/Scripts/vendors/angular/angular.min.js",
                "~/Scripts/vendors/angular-loading-bar/build/loading-bar.min.js",
                "~/Scripts/vendors/angular-ui-router.min.js",
                "~/Scripts/vendors/ui-bootstrap-tpls-2.0.0.min.js",
                "~/Scripts/vendors/angular-ui-router-uib-modal/angular-ui-router-uib-modal.js",
                "~/Scripts/vendors/tg-angular-validator/dist/angular-validator.min.js",
                "~/Scripts/vendors/angular-cookies/angular-cookies.min.js",
                "~/Scripts/vendors/angular-local-storage/dist/angular-local-storage.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app.js",
                "~/Scripts/directives/navbarDirective.js",
                "~/Scripts/services/apiService.js",
                "~/Scripts/services/authService.js",
                "~/Scripts/directives/availableMovie.js",
                "~/Scripts/controllers/loginCtrl.js",
                "~/Scripts/controllers/cartCtrl.js",
                "~/Scripts/controllers/editMovieCtrl.js",
                "~/Scripts/controllers/indexController.js",
                "~/Scripts/controllers/addMovieCtrl.js",
                "~/Scripts/controllers/moviesCtrl.js",
                "~/Scripts/controllers/movieDetails.js",
                "~/Scripts/controllers/registerCtrl.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/style.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/fonts/fontawesome-webfont.woff",
                "~/Content/css/loading-bar.min.css"
            ));
        }
    }
}