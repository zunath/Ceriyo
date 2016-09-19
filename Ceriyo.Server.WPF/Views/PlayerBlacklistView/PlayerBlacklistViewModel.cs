using System.ComponentModel;
using Prism.Commands;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.PlayerBlacklistView
{
    public class PlayerBlacklistViewModel : BindableBase
    {
        public PlayerBlacklistViewModel()
        {
            Players = new BindingList<string>();
            AddToBlacklistCommand = new DelegateCommand(AddToBlacklist);
            RemoveSelectedCommand = new DelegateCommand(RemoveSelected);
            BlacklistUsername = string.Empty;
        }

        private BindingList<string> _players;

        public BindingList<string> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        private string _blacklistUsername;

        public string BlacklistUsername
        {
            get { return _blacklistUsername; }
            set { SetProperty(ref _blacklistUsername, value); }
        }

        public DelegateCommand AddToBlacklistCommand { get; set; }
        public DelegateCommand RemoveSelectedCommand { get; set; }

        private void AddToBlacklist()
        {
            
        }

        private void RemoveSelected()
        {
            
        }

    }
}
