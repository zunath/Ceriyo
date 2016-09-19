using Ceriyo.Toolset.WPF.Events;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ApplicationRootView
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel()
        {

        }

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

    }
}
