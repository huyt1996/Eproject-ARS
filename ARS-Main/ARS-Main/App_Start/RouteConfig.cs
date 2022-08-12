using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ARS_Main
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AirplaneTypes",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AirplaneTypes", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Airplanes",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Airplanes", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Flights",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Flights", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "FlightSchedules",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FlightSchedules", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
