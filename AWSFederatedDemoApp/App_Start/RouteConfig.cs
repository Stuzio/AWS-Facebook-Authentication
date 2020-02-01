using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AWSFederatedDemoApp
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
                name: "FacebookDemoRout",
                url: "FacebookDemo/{action}/{id}",
                defaults: new { controller = "FacebookDemo", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "IAMUserSDKRout",
                url: "IAMUserSDK/{action}/{id}",
                defaults: new { controller = "IAMUserSDK", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
