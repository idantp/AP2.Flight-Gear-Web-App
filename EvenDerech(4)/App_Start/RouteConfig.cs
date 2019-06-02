using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EvenDerech_4_
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "LocatePlane",
                url: "display/{ip}/{port}",
                defaults: new { controller = "Flight", action = "LocatePlane" }
                );
            routes.MapRoute(
                name: "FlightPath",
                url: "display/{ip}/{port}/{rate}",
                defaults: new { controller = "Flight", action = "FlightPath" }
                );
            routes.MapRoute(
                name: "SaveFlightData",
                url: "save/{ip}/{port}/{rate}/{duration}/{path}",
                defaults: new { controller = "Flight", action = "SaveFlightData" }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Flight", action = "StartScreen", id = UrlParameter.Optional }
            );
            
        }
    }
}
