﻿using System.Web.Optimization;

namespace WebAppAuth
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //"~/Scripts/bootstrap.js",
            //"~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/*.js"));

            //    bundles.Add(new StyleBundle("~/Content/css")
            //        .Include("~/Content/css/*.css")
            //        .Include("~/Content/css/*.scss")
            //        .Include("~/Content/css/presets/*.css"));
            //
        }
    }
}
