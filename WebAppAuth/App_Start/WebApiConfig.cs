using System.Web.Http;

namespace WebAppAuth
{
    /// <summary>
    /// Конфигурация Web Api
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            config.Formatters.Add(GlobalConfiguration.Configuration.Formatters.JsonFormatter);
        }
    }
}