using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using System.Web.Http.Dependencies;
using System.Web.Http.Cors;
using Assignment2.API.Authentication;

namespace Assignment2.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new AuthenticationHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.DependencyResolver = new UnityResolver(UnityConfig.Container);

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
        }
    }
}
