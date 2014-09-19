﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SeguimientoEleccion.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Fiscal", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                              "appcache",
                              "offline.appcache",
                              new { controller = "offline", action = "index" });
        }
    }
}
