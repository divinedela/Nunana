﻿using System.Web.Optimization;

namespace Nunana
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                "~/Scripts/datatables/jquery.datatables.js",
                "~/Scripts/datatables/datatables.bootstrap.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/typeahead.bundle.js",
                "~/Scripts/sweetalert2.all.js",
                "~/Scripts/chart.js"
               ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/stylish-portfolio.css"));

            bundles.Add(new StyleBundle("~/Content/libcss").Include(
                "~/Content/datatables/css/datatables.bootstrap.css",
                "~/Content/typeahead.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/sweetalert/sweet-alert.css"));
        }
    }
}