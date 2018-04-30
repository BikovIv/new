using System.Net.Http.Headers;
using System.Web.Http;

namespace LostStuffsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            config.Formatters.JsonFormatter.SupportedMediaTypes
                 .Add(new MediaTypeHeaderValue("text/html"));

            config.Formatters.XmlFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("multipart/form-data"));


            config.Formatters.XmlFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("application/vnd.api+json"));



            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name: "MyRoute",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
        }
    }
}
