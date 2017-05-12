using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Settings;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.DetailsView
{
    public class DetailsViewModel : BindableBase
    {
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly Timer _queueTimer;

        public DetailsViewModel()
        {
            
        }
        
        public DetailsViewModel(IDataService dataService,
            IPathService pathService, 
            ServerSettings settings)
        {
            _dataService = dataService;
            _pathService = pathService;
            _settings = settings;

            Modules = new BindingList<string>();
            Players = new BindingList<string>();
            MaximumPortNumber = short.MaxValue;
            MaximumPlayers = 50;
            
            _queueTimer = new Timer(2000);
            _queueTimer.Elapsed += ProcessServerActions;

            StartStopServerButtonText = "Start Server";

            BanAccountCommand = new DelegateCommand(BanUsername);
            BootPlayerCommand = new DelegateCommand(BootPlayer);
            SendMessageCommand = new DelegateCommand(SendMessage);
            SaveSettingsCommand = new DelegateCommand(SaveSettings);
            ToggleServerCommand = new DelegateCommand(ToggleServer);

            NoModuleNotification = new InteractionRequest<INotification>();
            LoadModules();
            
        }

        private void LoadModules()
        {
            Modules.Clear();

            foreach (var file in Directory.GetFiles(_pathService.ModuleDirectory, "*.mod"))
            {
                Modules.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void ProcessServerActions(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _serverGame.RefreshSettings(Settings);
        }

        public InteractionRequest<INotification> NoModuleNotification { get; }

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
        
        private BindingList<string> _modules;

        public BindingList<string> Modules
        {
            get { return _modules; }
            set { SetProperty(ref _modules, value); }
        }

        private string _selectedModule;

        public string SelectedModule
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

        private string _startStopServerButtonText;

        public string StartStopServerButtonText
        {
            get { return _startStopServerButtonText; }
            set { SetProperty(ref _startStopServerButtonText, value); }
        }

        private bool _isServerRunning;

        public bool IsServerRunning
        {
            get { return _isServerRunning; }
            set { SetProperty(ref _isServerRunning, value); }
        }

        private ServerGame _serverGame;


        public DelegateCommand BanAccountCommand { get; set; }
        public DelegateCommand BootPlayerCommand { get; set; }
        public DelegateCommand SendMessageCommand { get; set; }
        public DelegateCommand SaveSettingsCommand { get; set; }
        public DelegateCommand ToggleServerCommand { get; set; }

        private void BanUsername()
        {
            if (string.IsNullOrWhiteSpace(SelectedPlayer)) return;

            _serverGame.BanUsername(SelectedPlayer);

            if (_settings.Blacklist.Contains(SelectedPlayer)) return;

            _settings.Blacklist.Add(SelectedPlayer);
        }
        
        private void BootPlayer()
        {
            if (string.IsNullOrWhiteSpace(SelectedPlayer)) return;

            _serverGame.BootUsername(SelectedPlayer);
        }

        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(ServerMessage)) return;

            _serverGame.SendServerMessage(ServerMessage);
        }

        private void SaveSettings()
        {
            _dataService.Save(_settings);
        }

        private void ToggleServer()
        {
            if (IsServerRunning)
            {
                _serverGame.Exit();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(SelectedModule))
                {
                    NoModuleNotification.Raise(new Notification
                    {
                        Title = "No Module Selected",
                        Content = "No module was selected. Please select a module and click 'Start Server' again."
                    });
                    return;
                }

                try
                {
                    StartStopServerButtonText = "Stop Server";
                    StartServerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    StartStopServerButtonText = "Start Server";
                    IsServerRunning = false;
                    _queueTimer.Enabled = false;
                    Players.Clear();
                }
            }
            
        }

        private async void StartServerAsync()
        {
            await Task.Run(() =>
            {
                IsServerRunning = true;
                _queueTimer.Enabled = true;
                using (_serverGame = new ServerGame(_settings, SelectedModule))
                {
                    _serverGame.OnPlayerConnected += PlayerConnected;
                    _serverGame.OnPlayerDisconnected += PlayerDisconnected;
                    _serverGame.Run();
                }
                StartStopServerButtonText = "Start Server";
                _queueTimer.Enabled = false;
                IsServerRunning = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Players.Clear();
                });
            });
        }

        private void PlayerConnected(string username)
        {
            if (!Players.Contains(username))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Players.Add(username);
                });
            }
        }
        private void PlayerDisconnected(string username)
        {
            if (Players.Contains(username))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Players.Remove(username);
                });
            }
        }

    }
}
