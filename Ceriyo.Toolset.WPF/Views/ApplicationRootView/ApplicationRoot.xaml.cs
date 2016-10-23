using System.ComponentModel;
using System.Windows;
using Ceriyo.Toolset.WPF.Events.Application;
using Ceriyo.Toolset.WPF.Events.Module;
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

        public ApplicationRoot(IEventAggregator eventAggregator,
            ToolsetGame game)
        {
            InitializeComponent();

            GameGrid.Children.Add(game);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ApplicationClosedEvent>().Subscribe(CloseApplication);
        }

        private void CloseApplication()
        {
            _eventAggregator.GetEvent<ModuleClosedEvent>().Publish();
            Application.Current.Shutdown();
        }

        private void ApplicationRoot_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            _eventAggregator.GetEvent<ApplicationClosedEvent>().Publish();
        }

        private void ApplicationRoot_OnLoaded(object sender, RoutedEventArgs e)
        {
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Publish();
        }
    }
}
