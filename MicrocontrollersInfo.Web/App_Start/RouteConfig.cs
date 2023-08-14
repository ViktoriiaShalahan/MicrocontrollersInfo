using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MicrocontrollersInfo.Web
{
    public class RouteConfig
    {
        public const string ALL_VALUES = "...";
        public const string ALL_PAGES = "..";
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "Мікроконтролери",
                defaults: new
                {
                    controller = "Microcontrollers",
                    action = "InfoWithPaging",
                    pageKey = "..",
                    pageNumber = 0
                }
            );

            routes.MapRoute(
                name: null,
                url: "Мікроконтролери_{pageKey}",
                defaults: new
                {
                    controller = "Microcontrollers",
                    action = "InfoWithPaging",
                    pageNumber = 0
                },
                constraints: new { pageKey = @"[A-Z]" }
            );

            routes.MapRoute(
                name: null,
                url: "Мікроконтролери_{pageNumber}",
                defaults: new
                {
                    controller = "Microcontrollers",
                    action = "InfoWithPaging",
                    pageKey = ".."
                },
                constraints: new { pageNumber = @"\d+" }
            );

            routes.MapRoute(
                name: null,
                url: "Мікроконтролери_{pageKey}_{pageNumber}",
                defaults: new
                {
                    controller = "Microcontrollers",
                    action = "InfoWithPaging"
                },
                constraints: new { pageKey = @"[A-Z]", pageNumber = @"\d+" }
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
