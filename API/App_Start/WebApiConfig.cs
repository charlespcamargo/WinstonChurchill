using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http; 
using WinstonChurchill.API.Custom;

namespace WinstonChurchill.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.SetCorsPolicyProviderFactory(new CorsPolicyFactory());
            config.EnableCors();


            // https://code.msdn.microsoft.com/Loop-Reference-handling-in-caaffaf7
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;


            //deixando somente retorno em JSON e indentando o JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;

        }
    }
}
