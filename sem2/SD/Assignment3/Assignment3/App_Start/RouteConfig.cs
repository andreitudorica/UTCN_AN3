using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assignment3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               "Student_default",
               "Student/{controller}/{action}/{id}",
               new { controller = "Student", action = "Index", id = UrlParameter.Optional },
               new[] { "Assignment3.Areas.Student.Controllers" }
           ).DataTokens.Add("area", "Student");

            routes.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                new[] { "Assignment3.Areas.Admin.Controllers" }
            ).DataTokens.Add("area", "Admin");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Assignment3.Controllers" }
            );//.DataTokens.Add("area", "Student");
           
        }
    }
}
