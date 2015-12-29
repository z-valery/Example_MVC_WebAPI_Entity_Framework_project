using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartHouse
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ChangeMvcWebApi",
                url: "Home/MvcWebApi/{mvcWebApi}",
                defaults: new { controller = "Home", action = "MvcWebApi" }
            );
            routes.MapRoute(
                name: "AddDevice",
                url: "Home/Create/{device}",
                defaults: new { controller = "Home", action = "Create" }
            );
            routes.MapRoute(
                name: "Console",
                url: "Home/Console/{command}",
                defaults: new { controller = "Home", action = "Console" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{device}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, device = UrlParameter.Optional }
            );
        }
    }
}
