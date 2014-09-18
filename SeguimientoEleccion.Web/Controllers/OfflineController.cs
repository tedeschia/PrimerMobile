using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using SeguimientoEleccion.Web.Helpers;

namespace SeguimientoEleccion.Web.Controllers
{
    public class OfflineController : Controller
    {
        // GET: Offline
        public ActionResult Index()
        {
            return new AppCacheResult(new[]
                                      {
                                          Url.Content("~/Scripts/App/Fiscal/Index.js"),
                                          Url.Content("~/Scripts/App/utils.js"),
                                          Url.Content("~/Scripts/App/Data/dataContext.js"),
                                          Url.Content("~/fonts/glyphicons-halflings-regular.ttf"),
                                          Url.Content("~/fonts/glyphicons-halflings-regular.woff"),
                                          BundleTable.Bundles.ResolveBundleUrl("~/bundles/jquery"),
                                          BundleTable.Bundles.ResolveBundleUrl("~/bundles/modernizr"),
                                          BundleTable.Bundles.ResolveBundleUrl("~/Content/css"),
                                          BundleTable.Bundles.ResolveBundleUrl("~/bundles/bootstrap"),
                                          BundleTable.Bundles.ResolveBundleUrl("~/bundles/knockout"),
                                          BundleTable.Bundles.ResolveBundleUrl("~/bundles/fastClick"),
                                          BundleTable.Bundles.ResolveBundleUrl("~/bundles/app")
                                      }
                ,
                fingerprint: BundleTable.Bundles
                    .FingerprintsOf(
                        "~/Content/css", "~/bundles/app"));
        }
    }
}