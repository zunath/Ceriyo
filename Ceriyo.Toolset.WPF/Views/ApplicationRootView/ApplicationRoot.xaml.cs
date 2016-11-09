using System.ComponentModel;
using System.Windows;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Settings;
using Ceriyo.Toolset.WPF.Events.Application;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.GameWorld;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Views.ApplicationRootView
{
    public partial class ApplicationRoot
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private ToolsetSettings _settings;

        public ApplicationRoot()
        {
            InitializeComponent();
        }

        public ApplicationRoot(IEventAggregator eventAggregator,
            ToolsetGame game,
            IDataService dataService)
        {
            InitializeComponent();

            GameGrid.Children.Add(game);
            _dataService = dataService;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ApplicationClosedEvent>().Subscribe(CloseApplication);
        }

        private void CloseApplication()
        {
            SaveSettings();
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
            _settings = _dataService.Load<ToolsetSettings>();
            ApplySavedSettings();
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Publish();
        }
        private void ApplySavedSettings()
        {
            if (_settings.Width > 0)
                Width = _settings.Width;
            if (_settings.Height > 0)
                Height = _settings.Height;
            if (_settings.PositionX >= 0)
                Left = _settings.PositionX;
            if (_settings.PositionY >= 0)
                Top = _settings.PositionY;

            if(_settings.IsMaximized)
                WindowState = WindowState.Maximized;
        }

        private void SaveSettings()
        {
            _settings.Width = (int)Width;
            _settings.Height = (int)Height;
            _settings.PositionX = (int)Left;
            _settings.PositionY = (int)Top;
            _settings.IsMaximized = WindowState == WindowState.Maximized;

            _dataService.Save(_settings);
        }
    }
}
