using System;
using System.IO;
using Ceriyo.Domain.Services.Contracts;
using Ceriyo.Domain.Services.DataServices;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events;
using Prism.Events;

namespace Ceriyo.Toolset.WPF
{
    public class DomainServiceNotifier : IDomainServiceNotifier
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDomainService _moduleDomainService;

        public DomainServiceNotifier(
            IEventAggregator eventAggregator,
            IModuleDomainService moduleDomainService)
        {
            _eventAggregator = eventAggregator;
            _moduleDomainService = moduleDomainService;
        }

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            // Module Events
            _eventAggregator.GetEvent<ModuleCreatedEvent>().Subscribe(ModuleCreated);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
            _eventAggregator.GetEvent<ModulePropertiesChangedEvent>().Subscribe(ModulePropertiesChanged);
            _eventAggregator.GetEvent<ModuleOpenedEvent>().Subscribe(ModuleOpened);
            _eventAggregator.GetEvent<ModuleSavedEvent>().Subscribe(ModuleSaved);
        }

        private void ModuleCreated(ModuleEventArgs e)
        {
            _moduleDomainService.CreateModule(e.Name, e.Tag, e.Resref);
        }

        private void ModuleClosed()
        {
            _moduleDomainService.CloseModule();
        }

        private void ModulePropertiesChanged()
        {
            _moduleDomainService.SaveModuleProperties();
        }

        private void ModuleOpened(string fileName)
        {
            try
            {
                _moduleDomainService.OpenModule(fileName);
                _eventAggregator.GetEvent<ModuleLoadedEvent>().Publish(fileName);
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Unable to load module.", ex);
            }
        }

        private void ModuleSaved(string moduleFileName)
        {
            _moduleDomainService.PackModule(moduleFileName);
        }

    }
}
