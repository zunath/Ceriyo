using Ceriyo.Domain.Services.Contract;
using Ceriyo.Domain.Services.DataServices;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events;
using Prism.Events;

namespace Ceriyo.Toolset.WPF
{
    public class DomainServiceNotifier: IDomainServiceNotifier
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDomainService _moduleDomainService;

        public DomainServiceNotifier(
            IEventAggregator eventAggregator,
            IModuleDomainService moduleDomainService)
        {
            _eventAggregator = eventAggregator;
            _moduleDomainService = moduleDomainService;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<ModuleCreatedEvent>().Subscribe(ModuleCreated);
        }

        private void ModuleCreated(ModuleEventArgs e)
        {
            _moduleDomainService.CreateModule(e.Name, e.Tag, e.Resref);
        }

    }
}
