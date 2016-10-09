using System;
using System.Windows;
using System.Windows.Threading;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Toolset.WPF.Events.Application;
using Ceriyo.Toolset.WPF.Events.Error;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ApplicationRootView
{
    public class ApplicationRootViewModel : BindableBase
    {
        private readonly ILogger _logger;
        private readonly IModuleDomainService _moduleDomainService;

        public ApplicationRootViewModel()
        {

        }

        public ApplicationRootViewModel(
            ILogger logger,
            IEventAggregator eventAggregator,
            IModuleDomainService moduleDomainService)
        {
            _logger = logger;
            _moduleDomainService = moduleDomainService;
            
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.Current.Dispatcher.UnhandledException += DispatcherOnUnhandledException;

            ErrorNotificationRequest = new InteractionRequest<INotification>();

            eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(ChangeWindowTitle);
            eventAggregator.GetEvent<ModulePropertiesChangedEvent>().Subscribe(ChangeWindowTitle);
            eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ChangeWindowTitle);
        }

        private void ModuleLoaded(string moduleFileName)
        {
            ChangeWindowTitle();
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleError(sender, e.Exception);
            e.Handled = true;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            HandleError(sender, ex);
        }

        private void HandleError(object sender, Exception ex)
        {
            _logger.Error($"Global exception unhandled. Sender: {sender}", ex);

            ErrorNotificationRequest.Raise(new Notification
            {
                Title = "Error Occurred",
                Content = $"An error has occured. Details: {ex?.Message}"
            });
        }

        public InteractionRequest<INotification> ErrorNotificationRequest { get; }

        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { SetProperty(ref _windowTitle, value); }
        }

        private void ChangeWindowTitle()
        {
            ModuleData moduleData = _moduleDomainService.GetLoadedModuleData();

            if (string.IsNullOrWhiteSpace(moduleData?.Name))
            {
                WindowTitle = "Ceriyo Toolset";
            }
            else
            {
                WindowTitle = "Ceriyo Toolset - " + moduleData.Name;
            }
        }

    }
}
