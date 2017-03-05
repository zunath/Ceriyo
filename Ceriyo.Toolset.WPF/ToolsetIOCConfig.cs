using System;
using Artemis;
using Autofac;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Entities.Contracts;
using Ceriyo.Core.Scripting.Common;
using Ceriyo.Core.Scripting.Common.Contracts;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Core.Scripting.Server.Contracts;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Services.Game;
using Ceriyo.Core.Services.Module;
using Ceriyo.Domain.Services.DataServices;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.Helpers;
using Ceriyo.Infrastructure.Logging;
using Ceriyo.Infrastructure.Services;
using Ceriyo.Infrastructure.WPF.Factory;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Validation;
using Ceriyo.Infrastructure.WPF.Validation.Contracts;
using Ceriyo.Toolset.WPF.Contracts;
using Ceriyo.Toolset.WPF.GameWorld;
using Ceriyo.Toolset.WPF.Mapping;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ceriyo.Toolset.WPF
{
    public static class ToolsetIOCConfig
    {
        public static void Initialize(ContainerBuilder builder)
        {
            // Logging
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();

            // Artemis
            builder.RegisterType<EntityWorld>().SingleInstance();
            IOCHelpers.RegisterSystems(builder);
            builder.RegisterType<ToolsetSystemLoader>().As<ISystemLoader>().SingleInstance();
    
            // Services
            builder.RegisterType<AppService>().As<IAppService>().SingleInstance();
            builder.RegisterType<DataService>().As<IDataService>().SingleInstance();
            builder.RegisterType<ToolsetGameService>().As<IGameService>().SingleInstance();
            builder.RegisterType<ScreenService>().As<IScreenService>().SingleInstance();
            builder.RegisterType<GraphicsService>().As<IGraphicsService>().SingleInstance();
            builder.RegisterType<PathService>().As<IPathService>().SingleInstance();
            builder.RegisterType<ToolsetInputService>().As<IInputService>().SingleInstance();
            builder.RegisterType<ModuleDataService>().As<IModuleDataService>().SingleInstance();
            builder.RegisterType<ModuleResourceService>().As<IModuleResourceService>();
            builder.RegisterType<ModuleService>().As<IModuleService>().SingleInstance();
            builder.RegisterType<EngineService>().As<IEngineService>().SingleInstance();

            // Factory
            builder.RegisterType<EntityFactory>().As<IEntityFactory>().SingleInstance();
            builder.RegisterType<ComponentFactory>().As<IComponentFactory>().SingleInstance();
            builder.RegisterType<ScreenFactory>().As<IScreenFactory>();
            builder.RegisterType<ObservableDataFactory>().As<IObservableDataFactory>();
            
            // Scripting
            builder.RegisterType<LoggingMethods>().As<ILoggingMethods>().SingleInstance();
            builder.RegisterType<EntityMethods>().As<IEntityMethods>().SingleInstance();
            builder.RegisterType<LocalDataMethods>().As<ILocalDataMethods>().SingleInstance();
            builder.RegisterType<PhysicsMethods>().As<IPhysicsMethods>().SingleInstance();
            builder.RegisterType<ScriptingMethods>().As<IScriptingMethods>().SingleInstance();

            // Mapping
            builder.RegisterType<ToolsetObjectMapper>().As<IObjectMapper>();

            // Domain Services
            builder.RegisterType<DomainServiceNotifier>().As<IDomainServiceNotifier>().SingleInstance();
            builder.RegisterType<DataEditorDomainService>().As<IDataEditorDomainService>();
            builder.RegisterType<ResourceEditorDomainService>().As<IResourceEditorDomainService>();
            builder.RegisterType<AreaDomainService>().As<IAreaDomainService>().SingleInstance();

            // Validation
            builder.RegisterType<ValidationHelper>().As<IValidationHelper>();   

            // Game components
            IOCHelpers.RegisterScreens(builder);
            IOCHelpers.RegisterComponents(builder);

            // Entities
            builder.RegisterType<Area>().As<IGameEntity<AreaData>>();
            builder.RegisterType<ObjectPainter>().As<IGameEntity<Texture2D>>();
            
            // MonoGame
            RegisterMonogame(builder);
        }
        
        private static void RegisterMonogame(ContainerBuilder builder)
        {
            // Create Direct3D 11 device.
            var presentationParameters = new PresentationParameters
            {
                // Do not associate graphics device with window.
                DeviceWindowHandle = IntPtr.Zero,
            };
            var device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, presentationParameters);

            builder.RegisterInstance(device).AsSelf();
            builder.RegisterInstance(new SpriteBatch(device)).AsSelf();
            builder.RegisterInstance(new Camera2D(device)).AsSelf();
               
            var game = new ToolsetGame(device);
            builder.RegisterInstance(game);
            builder.RegisterType<Texture2D>();
            builder.RegisterInstance(game.Content).AsSelf();
        }
    }
}
