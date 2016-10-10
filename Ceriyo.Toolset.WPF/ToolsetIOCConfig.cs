using System;
using System.Linq;
using Artemis;
using Autofac;
using Ceriyo.Core.Contracts;
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
using Ceriyo.Infrastructure.Logging;
using Ceriyo.Infrastructure.Mapping;
using Ceriyo.Infrastructure.Services;
using FluentValidation;
using Microsoft.Xna.Framework.Graphics;
using IValidatorFactory = Ceriyo.Core.Contracts.IValidatorFactory;

namespace Ceriyo.Toolset.WPF
{
    public class ToolsetIOCConfig
    {
        public static void Initialize(ContainerBuilder builder)
        {
            // Instances
            builder.RegisterInstance(new ToolsetSettings());

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
            builder.RegisterType<ValidatorFactory>().As<IValidatorFactory>();
            builder.RegisterType<ModuleFactory>().As<IModuleFactory>();

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
            builder.RegisterType<ObjectMapper>().As<IObjectMapper>();

            // Domain Services
            builder.RegisterType<DomainServiceNotifier>().As<IDomainServiceNotifier>().SingleInstance();
            builder.RegisterType<ModuleDomainService>().As<IModuleDomainService>().SingleInstance();
            builder.RegisterType<DataEditorDomainService>().As<IDataEditorDomainService>();
            builder.RegisterType<ResourceEditorDomainService>().As<IResourceEditorDomainService>();

            // Validators
            RegisterValidators(builder);

        }

        private static void RegisterValidators(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var validators = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(p =>
                    p.BaseType != null &&
                    p.BaseType.IsGenericType &&
                    p.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>));

            foreach (var validator in validators)
            {
                if (validator.BaseType == null) continue;
                var type = validator.BaseType.GetGenericArguments()[0];

                builder.RegisterType(validator)
                    .Keyed<IValidator>(type)
                    .As<IValidator>();
            }
        }

    }
}
