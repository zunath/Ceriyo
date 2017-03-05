using Artemis;
using Autofac;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Scripting.Common;
using Ceriyo.Core.Scripting.Common.Contracts;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Core.Scripting.Server.Contracts;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Services.Game;
using Ceriyo.Core.Settings;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.Logging;
using Ceriyo.Infrastructure.Services;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Server.WPF
{
    public class ServerIOCConfig
    {
        public static void Initialize(ContainerBuilder builder)
        {
            // Instances
            builder.RegisterInstance(new ServerSettings());
            
            // Logging
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();

            // MonoGame
            builder.RegisterType<Texture2D>();

            // Services
            builder.RegisterType<AppService>().As<IAppService>();
            builder.RegisterType<DataService>().As<IDataService>();
            builder.RegisterType<ScreenService>().As<IScreenService>();
            builder.RegisterType<GraphicsService>().As<IGraphicsService>();
            builder.RegisterType<PathService>().As<IPathService>();

            // Artemis
            builder.RegisterType<EntityWorld>().SingleInstance();

            // Factory
            builder.RegisterType<EntityFactory>().As<IEntityFactory>().SingleInstance();
            builder.RegisterType<ComponentFactory>().As<IComponentFactory>().SingleInstance();
            builder.RegisterType<ScreenFactory>().As<IScreenFactory>();

            // Scripting
            builder.RegisterType<LoggingMethods>().As<ILoggingMethods>().SingleInstance();
            builder.RegisterType<EntityMethods>().As<IEntityMethods>().SingleInstance();
            builder.RegisterType<LocalDataMethods>().As<ILocalDataMethods>().SingleInstance();
            builder.RegisterType<PhysicsMethods>().As<IPhysicsMethods>().SingleInstance();
            builder.RegisterType<ScriptingMethods>().As<IScriptingMethods>().SingleInstance();
            builder.RegisterType<ScriptService>().As<IScriptService>()
                .WithParameter("isServer", true)
                .SingleInstance();
        }
    }
}
