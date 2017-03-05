using System;
using System.Linq;
using Artemis;
using Autofac;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Entities.Contracts;
using Ceriyo.Core.Scripting.Common;
using Ceriyo.Core.Scripting.Common.Contracts;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Core.Scripting.Server.Contracts;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Services.Game;
using Ceriyo.Core.Settings;
using Ceriyo.Game.Windows.Services;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.Helpers;
using Ceriyo.Infrastructure.Logging;
using Ceriyo.Infrastructure.Services;
using EmptyKeys.UserInterface;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ceriyo.Game.Windows
{
    public class GameIOCConfig
    {
        private static IContainer _container;

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
        public static void Initialize(Microsoft.Xna.Framework.Game game)
        {
            var builder = new ContainerBuilder();
            game.Content.RootDirectory = "Compiled";

            // Monogame 
            builder.RegisterInstance(game);
            builder.RegisterInstance(new SpriteBatch(game.GraphicsDevice)).AsSelf();
            builder.RegisterInstance(game.Content).AsSelf();
            builder.RegisterInstance(game.GraphicsDevice).AsSelf();
            builder.RegisterInstance(new GameSettings());
            builder.RegisterType<Texture2D>();
            builder.RegisterInstance(new Camera2D(game.GraphicsDevice)).AsSelf();

            // Logging
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            
            // Services
            builder.RegisterType<AppService>().As<IAppService>().SingleInstance();
            builder.RegisterType<DataService>().As<IDataService>().SingleInstance();
            builder.RegisterType<GameService>().As<IGameService>().SingleInstance();
            builder.RegisterType<ScreenService>().As<IScreenService>().SingleInstance();
            builder.RegisterType<GraphicsService>().As<IGraphicsService>().SingleInstance();
            builder.RegisterType<PathService>().As<IPathService>().SingleInstance();
            builder.RegisterType<GameInputService>().As<IInputService>().SingleInstance();
            builder.RegisterType<EngineService>().As<IEngineService>().SingleInstance();
            builder.RegisterType<UIService>().As<IUIService>().SingleInstance();

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
                .WithParameter("isServer", false)
                .SingleInstance();
            
            // Game components
            RegisterGameEntities(builder);
            IOCHelpers.RegisterComponents(builder);
            IOCHelpers.RegisterSystems(builder);
            IOCHelpers.RegisterScreens(builder);
            
            
            _container = builder.Build();
        }


        private static void RegisterGameEntities(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var gameEntities = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IGameEntity).IsAssignableFrom(p) && p.IsClass).ToArray();
            foreach (Type type in gameEntities)
            {
                builder.RegisterType(type).As<IGameEntity>().Named<IGameEntity>(type.ToString());
            }

        }
        
        
    }
}
