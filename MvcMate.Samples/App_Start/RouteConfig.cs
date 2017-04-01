using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcMate.Web.Routing;

namespace MvcMate.Samples
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Atom",
                url: "article/atom",
                defaults: new { controller = "ActionResults", action = "Atom10" }
            );

            routes.MapRoute(
                name: "Rss",
                url: "article/rss",
                defaults: new { controller = "ActionResults", action = "Rss20" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = new NotEqualConstraint("Content") }
            );
        }
    }
}