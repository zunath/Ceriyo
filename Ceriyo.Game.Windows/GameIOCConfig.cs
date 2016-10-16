﻿using System;
using System.Linq;
using Artemis;
using Artemis.Interface;
using Artemis.System;
using Autofac;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Entities.Contracts;
using Ceriyo.Core.Scripting.Client;
using Ceriyo.Core.Scripting.Client.Contracts;
using Ceriyo.Core.Scripting.Common;
using Ceriyo.Core.Scripting.Common.Contracts;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Core.Scripting.Server.Contracts;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Settings;
using Ceriyo.Core.UI;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.Logging;
using Ceriyo.Infrastructure.Services;
using Microsoft.Xna.Framework.Graphics;
using Squid;

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
            game.Content.RootDirectory = "Content";

            // Monogame 
            builder.RegisterInstance(game);
            builder.RegisterInstance(new SpriteBatch(game.GraphicsDevice)).AsSelf();
            builder.RegisterInstance(game.Content).AsSelf();
            builder.RegisterInstance(game.GraphicsDevice).AsSelf();
            builder.RegisterInstance(new GameSettings());
            
            // Logging
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();

            // MonoGame
            builder.RegisterType<Texture2D>();

            // Services
            builder.RegisterType<AppService>().As<IAppService>();
            builder.RegisterType<CameraService>().As<ICameraService>();
            builder.RegisterType<DataService>().As<IDataService>();
            builder.RegisterType<GameService>().As<IGameService>();
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
            builder.RegisterType<ControlMethods>().As<IControlMethods>().SingleInstance();
            builder.RegisterType<StyleMethods>().As<IStyleMethods>().SingleInstance();
            builder.RegisterType<SceneMethods>().As<ISceneMethods>().SingleInstance();
            
            RegisterGameEntities(builder);
            RegisterComponents(builder);
            RegisterSystems(builder);

            // UI
            builder.RegisterType<SquidRenderer>().As<ISquidRenderer>();
            builder.RegisterType<UIService>().As<IUIService>().SingleInstance();

            // Scripting
            builder.RegisterType<ScriptService>().As<IScriptService>()
                .WithParameter("isServer", false)
                .SingleInstance();


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

        private static void RegisterComponents(ContainerBuilder builder)
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

        private static void RegisterSystems(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var systems = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(EntitySystem).IsAssignableFrom(p) && !p.ToString().StartsWith("Artemis"));
            foreach (Type type in systems)
            {
                builder.RegisterType(type).As<EntitySystem>().Named<EntitySystem>(type.ToString());
            }
        }

    }
}