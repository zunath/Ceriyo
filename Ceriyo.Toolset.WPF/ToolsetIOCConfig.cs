using Artemis;
using Autofac;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
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
using Ceriyo.Domain.Services.Contracts;
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
using Ceriyo.Toolset.WPF.Mapping;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset.WPF
{
    public static class ToolsetIOCConfig
    {
        private static IContainer _container;

        public static void Initialize(ContainerBuilder builder)
        {
            // Instances
            builder.RegisterInstance(new ToolsetSettings());

            // Logging
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();

            // MonoGame
            var game = new ToolsetGame();
            builder.RegisterInstance(game);
            builder.RegisterType<Texture2D>();
            builder.RegisterInstance(game.Content).AsSelf();
                
            // Services
            builder.RegisterType<AppService>().As<IAppService>().SingleInstance();
            builder.RegisterType<CameraService>().As<ICameraService>().SingleInstance();
            builder.RegisterType<DataService>().As<IDataService>().SingleInstance();
            builder.RegisterType<ToolsetGameService>().As<IGameService>().SingleInstance();
            builder.RegisterType<ScreenService>().As<IScreenService>().SingleInstance();
            builder.RegisterType<GraphicsService>().As<IGraphicsService>().SingleInstance();
            builder.RegisterType<PathService>().As<IPathService>().SingleInstance();
            builder.RegisterType<ToolsetInputService>().As<IInputService>().SingleInstance();

            // Artemis
            builder.RegisterType<EntityWorld>().SingleInstance();

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
            builder.RegisterType<ControlMethods>().As<IControlMethods>().SingleInstance();
            builder.RegisterType<StyleMethods>().As<IStyleMethods>().SingleInstance();
            builder.RegisterType<SceneMethods>().As<ISceneMethods>().SingleInstance();

            // Mapping
            builder.RegisterType<ToolsetObjectMapper>().As<IObjectMapper>();

            // Domain Services
            builder.RegisterType<DomainServiceNotifier>().As<IDomainServiceNotifier>().SingleInstance();
            builder.RegisterType<ModuleDomainService>().As<IModuleDomainService>().SingleInstance();
            builder.RegisterType<DataEditorDomainService>().As<IDataEditorDomainService>();
            builder.RegisterType<ResourceEditorDomainService>().As<IResourceEditorDomainService>();
            builder.RegisterType<ModuleResourceDomainService>().As<IModuleResourceDomainService>();
            builder.RegisterType<AreaDomainService>().As<IAreaDomainService>().SingleInstance();

            // Validation
            builder.RegisterType<ValidationHelper>().As<IValidationHelper>();   

            // Game components
            IOCHelpers.RegisterScreens(builder);
            IOCHelpers.RegisterComponents(builder);

            // Entities
            builder.RegisterType<Area>().As<IGameEntity<AreaData>>();
        }

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static void RegisterGraphicsDevice(GraphicsDevice device)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(device).AsSelf();
            builder.RegisterInstance(new SpriteBatch(device)).AsSelf();
            builder.Update(_container);
        }
    }
}
