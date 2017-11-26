using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Ceriyo.Master.Auth.Services;
using Ceriyo.Master.Auth.Services.Contracts;

namespace Ceriyo.Master.Auth
{
    public static class AuthIOCConfig
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();
            
            // ASP.NET
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Services
            builder.RegisterType<EmailService>().As<IEmailService>();

            return builder.Build();
        }
    }
}