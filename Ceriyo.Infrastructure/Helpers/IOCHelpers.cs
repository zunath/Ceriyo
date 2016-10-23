using System;
using System.Linq;
using Artemis.Interface;
using Artemis.System;
using Autofac;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Infrastructure.Helpers
{
    public class IOCHelpers
    {
        public static void RegisterScreens(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var screens = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IScreen).IsAssignableFrom(p) && p.IsClass);
            foreach (Type type in screens)
            {
                builder.RegisterType(type).As<IScreen>().Named<IScreen>(type.ToString());
            }
        }

        public static void RegisterComponents(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var components = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IComponent).IsAssignableFrom(p) && p.IsClass);
            foreach (Type type in components)
            {
                builder.RegisterType(type).As<IComponent>().Named<IComponent>(type.ToString());
            }
        }

        public static void RegisterSystems(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var systems = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(EntitySystem).IsAssignableFrom(p) && !p.ToString().StartsWith("Artemis"));
            foreach (Type type in systems)
            {
                builder.RegisterType(type);
            }
        }

    }
}
