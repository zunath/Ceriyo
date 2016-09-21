using System;
using System.Reflection;
using System.Windows;
using Autofac;
using Ceriyo.Infrastructure.IOC;
using Ceriyo.Server.WPF.Views.ApplicationRootView;
using Microsoft.Practices.ServiceLocation;
using Prism.Autofac;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF
{
    public class Bootstrapper: AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<ApplicationRoot>();
        }

        protected override void InitializeShell()
        {

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();


        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var assemblyPath = $"{Assembly.GetExecutingAssembly().GetName().Name}.Views.{viewType.Name}View.{viewType.Name}ViewModel";
                var type = Type.GetType(assemblyPath);

                return type;
            });
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            IOCConfig.InitializeServer(builder);
        }
    }
}
