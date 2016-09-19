using System.Windows;
using Ceriyo.Toolset.WPF.Events;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Views.ApplicationRootView
{
    public partial class ApplicationRoot
    {
        private readonly IEventAggregator _eventAggregator;

        public ApplicationRoot()
        {
            InitializeComponent();
        }

        public ApplicationRoot(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ApplicationClosedEvent>().Subscribe(CloseApplication);
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

    }
}
