using System.Web.Http;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.OAuth;

namespace Ceriyo.Master.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            BuildAutofac(config);
        }

        private static void BuildAutofac(HttpConfiguration config)
        {
            var container = AuthIOCConfig.Initialize();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
