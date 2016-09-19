using System.ComponentModel;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.DetailsView
{
    public class DetailsViewModel : BindableBase
    {
        public DetailsViewModel()
        {
            Modules = new BindingList<Module>();
            MaximumPortNumber = short.MaxValue;
            SelectedPortNumber = 5121;
            SelectedPVPType = PVPType.None;
            SelectedGameCategory = GameCategory.Action;

            BanAccountCommand = new DelegateCommand(BanAccount);
            BootPlayerCommand = new DelegateCommand(BootPlayer);
            SendMessageCommand = new DelegateCommand(SendMessage);
            SaveSettingsCommand = new DelegateCommand(SaveSettings);
            StartServerCommand = new DelegateCommand(StartServer);

        }

        private string _ipAddress;

        public string IPAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }

        private string _serverName;

        public string ServerName
        {
            get { return _serverName; }
            set { SetProperty(ref _serverName, value); }
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

        private int _selectedPortNumber;

        public int SelectedPortNumber
        {
            get { return _selectedPortNumber; }
            set { SetProperty(ref _selectedPortNumber, value); }
        }

        private PVPType _selectedPVPType;

        public PVPType SelectedPVPType
        {
            get { return _selectedPVPType; }
            set { SetProperty(ref _selectedPVPType, value); }
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

        private GameCategory _selectedGameCategory;

        public GameCategory SelectedGameCategory
        {
            get { return _selectedGameCategory; }
            set { SetProperty(ref _selectedGameCategory, value); }
        }

        private int _maximumPlayers;

        public int MaximumPlayers
        {
            get { return _maximumPlayers; }
            set { SetProperty(ref _maximumPlayers, value); }
        }

        private int _selectedMaxPlayers;

        public int SelectedMaxPlayers
        {
            get { return _selectedMaxPlayers; }
            set { SetProperty(ref _selectedMaxPlayers, value); }
        }

        private bool _allowCharacterDeletion;

        public bool AllowCharacterDeletion
        {
            get { return _allowCharacterDeletion; }
            set { SetProperty(ref _allowCharacterDeletion, value); }
        }

        private bool _allowFileDownloading;

        public bool AllowFileDownloading
        {
            get { return _allowFileDownloading; }
            set { SetProperty(ref _allowFileDownloading, value); }
        }

        private string _playerPassword;

        public string PlayerPassword
        {
            get { return _playerPassword; }
            set { SetProperty(ref _playerPassword, value); }
        }

        private string _gmPassword;

        public string GMPassword
        {
            get { return _gmPassword; }
            set { SetProperty(ref _gmPassword, value); }
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
