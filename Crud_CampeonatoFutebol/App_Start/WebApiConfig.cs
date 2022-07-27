using System.Web.Http;
namespace Crud_CampeonatoFutebol.App_Start
{
    public static class WebApiConfig
    {
        public const string GraphQlPath = "/graphql";

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}