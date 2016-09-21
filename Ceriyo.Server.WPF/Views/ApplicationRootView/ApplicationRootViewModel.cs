using Ceriyo.Core.Contracts;
using Ceriyo.Core.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.ApplicationRootView
{
    public class ApplicationRootViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly ServerSettings _settings;

        public ApplicationRootViewModel()
        {
        }

        public ApplicationRootViewModel(IEventAggregator eventAggregator,
            IDataService dataService,
            ServerSettings settings)
        {
            ApplicationLoadedCommand = new DelegateCommand(ApplicationLoaded);
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _settings = settings;
        }

        public DelegateCommand ApplicationLoadedCommand { get; set; }

        private void ApplicationLoaded()
        {
            ServerSettings loadedSettings = _dataService.Load<ServerSettings>();
            _settings.GameCategory = loadedSettings.GameCategory;
            _settings.AllowCharacterDeletion = loadedSettings.AllowCharacterDeletion;
            _settings.AllowFileDownloading = loadedSettings.AllowFileDownloading;
            _settings.Announcement = loadedSettings.Announcement;
            _settings.Blacklist = loadedSettings.Blacklist;
            _settings.Description = loadedSettings.Description;
            _settings.GMPassword = loadedSettings.GMPassword;
            _settings.MaxPlayers = loadedSettings.MaxPlayers;
            _settings.GameCategory = loadedSettings.GameCategory;
            _settings.PVPType = loadedSettings.PVPType;
            _settings.PlayerPassword = loadedSettings.PlayerPassword;
            _settings.Port = loadedSettings.Port;
            _settings.ServerName = loadedSettings.ServerName;


            _settings.Description = "this is set from app loaded";
        }
        
    }
}
