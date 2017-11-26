using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;

namespace Ceriyo.Master.Auth
{
    public static class AuthIOCConfig
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();
            
            // ASP.NET
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            return builder.Build();
        }
    }
}