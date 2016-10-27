using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Area;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.BlueprintSelectorView
{
    public class BlueprintSelectorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public BlueprintSelectorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AreaOpenedEvent>().Subscribe(AreaOpened);
            _eventAggregator.GetEvent<AreaClosedEvent>().Subscribe(AreaClosed);
        }

        private void AreaOpened(AreaDataObservable obj)
        {
            IsAreaOpened = true;
        }

        private void AreaClosed(AreaDataObservable obj)
        {
            IsAreaOpened = false;
        }

        private bool _isAreaOpened;

        public bool IsAreaOpened
        {
            get { return _isAreaOpened; }
            set { SetProperty(ref _isAreaOpened, value); }
        }

    }
}
