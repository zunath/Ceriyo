using System.ComponentModel;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.DetailsView
{
    public class DetailsViewModel : BindableBase
    {
        public DetailsViewModel()
        {
            
        }

        public DetailsViewModel(ServerSettings settings)
        {
            _settings = settings;
            Modules = new BindingList<Module>();
            MaximumPortNumber = short.MaxValue;
            MaximumPlayers = 50;

            BanAccountCommand = new DelegateCommand(BanAccount);
            BootPlayerCommand = new DelegateCommand(BootPlayer);
            SendMessageCommand = new DelegateCommand(SendMessage);
            SaveSettingsCommand = new DelegateCommand(SaveSettings);
            StartServerCommand = new DelegateCommand(StartServer);
        }

        private ServerSettings _settings;
        public ServerSettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        private string _ipAddress;

        public string IPAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }
        
        private BindingList<Module> _modules;

        public BindingList<Module> Modules
        {
            get { return _modules; }
            set { SetProperty(ref _modules, value); }
        }

        private Module _selectedModule;

        public Module SelectedModule
        {
            get { return _selectedModule; }
            set { SetProperty(ref _selectedModule, value); }
        }

        private int _maximumPortNumber;

        public int MaximumPortNumber
        {
            get { return _maximumPortNumber; }
            set { SetProperty(ref _maximumPortNumber, value); }
        }
        
        private BindingList<string> _players;

        public BindingList<string> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        private string _selectedPlayer;

        public string SelectedPlayer
        {
            get { return _selectedPlayer; }
            set { SetProperty(ref _selectedPlayer, value); }
        }
        
        private int _maximumPlayers;

        public int MaximumPlayers
        {
            get { return _maximumPlayers; }
            set { SetProperty(ref _maximumPlayers, value); }
        }
        
        private string _serverMessage;

        public string ServerMessage
        {
            get { return _serverMessage; }
            set { SetProperty(ref _serverMessage, value); }
        }

        private string _serverStatus;

        public string ServerStatus
        {
            get { return _serverStatus; }
            set { SetProperty(ref _serverStatus, value); }
        }

        public DelegateCommand BanAccountCommand { get; set; }
        public DelegateCommand BootPlayerCommand { get; set; }
        public DelegateCommand SendMessageCommand { get; set; }
        public DelegateCommand SaveSettingsCommand { get; set; }
        public DelegateCommand StartServerCommand { get; set; }

        private void BanAccount()
        {
            
        }

        private void BootPlayer()
        {
            
        }

        private void SendMessage()
        {
            
        }

        private void SaveSettings()
        {
        }

        private void StartServer()
        {
            
        }
        


    }
}
