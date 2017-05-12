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
using Ceriyo.Core.Services.Module;
using Ceriyo.Core.Settings;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.Logging;
using Ceriyo.Infrastructure.Network;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Services;
using Ceriyo.Server.WPF.Mapping;
using Ceriyo.Server.WPF.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Server.WPF
{
    public class ServerIOCConfig
    {
        private static IContainer _container;

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Initialize the IOC configuration for use with the ServerGame (server logic)
        /// </summary>
        /// <param name="game"></param>
        public static void Initialize(Game game)
        {
            var builder = new ContainerBuilder();
            game.Content.RootDirectory = "Compiled";

            // Monogame
            builder.RegisterInstance(game).SingleInstance();
            builder.RegisterInstance(game.Content).AsSelf();
            builder.RegisterInstance(game.GraphicsDevice).AsSelf();
            builder.RegisterType<Texture2D>();

            // Server logic specific services
            builder.RegisterType<ServerNetworkService>().As<IServerNetworkService>().SingleInstance();
            builder.RegisterType<ServerSettingsService>().As<IServerSettingsService>().SingleInstance();
            builder.RegisterType<ModuleService>().As<IModuleService>()
                .WithParameter("isRunningAsServer", true)
                .SingleInstance();

            builder.RegisterType<ModuleDataService>().As<IModuleDataService>()
                .WithParameter("isRunningAsServer", true)
                .SingleInstance();

            // Mapping
            builder.RegisterType<ServerObjectMapper>().As<IObjectMapper>().SingleInstance();

            // Common builds between GUI and server logic
            Initialize(builder);

            _container = builder.Build();
        }

        /// <summary>
        /// Initialize the IOC configuration for use with the server GUI
        /// </summary>
        /// <param name="builder"></param>
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
            builder.RegisterType<ServerGameService>().As<IGameService>();
            builder.RegisterType<EngineService>().As<IEngineService>();


            // Artemis
            builder.RegisterType<EntityWorld>().SingleInstance();

            // Factory
            builder.RegisterType<EntityFactory>().As<IEntityFactory>().SingleInstance();
            builder.RegisterType<ComponentFactory>().As<IComponentFactory>().SingleInstance();
            builder.RegisterType<ScreenFactory>().As<IScreenFactory>().SingleInstance();
            
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
