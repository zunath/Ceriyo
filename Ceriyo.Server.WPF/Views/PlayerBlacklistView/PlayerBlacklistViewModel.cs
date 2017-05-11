using Ceriyo.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.PlayerBlacklistView
{
    public class PlayerBlacklistViewModel : BindableBase
    {
        public PlayerBlacklistViewModel()
        {
            
        }

        public PlayerBlacklistViewModel(ServerSettings settings)
        {
            _settings = settings;
            AddToBlacklistCommand = new DelegateCommand(AddToBlacklist);
            RemoveSelectedCommand = new DelegateCommand(RemoveSelected);
            BlacklistUsername = string.Empty;
        }

        private ServerSettings _settings;

        public ServerSettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }
        private string _blacklistUsername;

        public string BlacklistUsername
        {
            get { return _blacklistUsername; }
            set { SetProperty(ref _blacklistUsername, value); }
        }

        private string _selectedPlayer;

        public string SelectedPlayer
        {
            get { return _selectedPlayer; }
            set { SetProperty(ref _selectedPlayer, value); }
        }

        public DelegateCommand AddToBlacklistCommand { get; set; }
        public DelegateCommand RemoveSelectedCommand { get; set; }

        private void AddToBlacklist()
        {
            if (string.IsNullOrWhiteSpace(BlacklistUsername)) return;

            _settings.Blacklist.Add(BlacklistUsername);
            BlacklistUsername = string.Empty;
        }

        private void RemoveSelected()
        {
            if (!_settings.Blacklist.Contains(SelectedPlayer)) return;

            _settings.Blacklist.Remove(SelectedPlayer);
        }
    }
}
