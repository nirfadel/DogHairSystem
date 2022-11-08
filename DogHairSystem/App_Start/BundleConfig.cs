﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace DogHairSystem.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            StyleBundle(bundles);
            ScriptBundle(bundles);
        }

        public static void StyleBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
            .Include("~/Content/bootstrap.css"));
        }

        public static void ScriptBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
            .Include("~/Scripts/jquery-{version}.js")
            .Include("~/Scripts/bootstrap.js"));
        }
    }
}
