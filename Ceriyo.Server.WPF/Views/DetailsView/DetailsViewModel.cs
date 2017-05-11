using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Settings;
using Ceriyo.Server.WPF.Actions;
using Ceriyo.Server.WPF.Contracts;
using Prism.Commands;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.DetailsView
{
    public class DetailsViewModel : BindableBase
    {
        private readonly IDataService _dataService;
        private readonly ConcurrentQueue<IServerAction> _actionQueue;
        private readonly Timer _queueTimer;
        
        public DetailsViewModel()
        {
            
        }

        public DetailsViewModel(IDataService dataService, 
            ServerSettings settings)
        {
            _dataService = dataService;
            _settings = settings;
            Modules = new BindingList<Module>();
            Players = new BindingList<string>();
            MaximumPortNumber = short.MaxValue;
            MaximumPlayers = 50;

            _actionQueue = new ConcurrentQueue<IServerAction>();
            _queueTimer = new Timer(2000);
            _queueTimer.Elapsed += ProcessServerActionsQueue;

            StartStopServerButtonText = "Start Server";

            BanAccountCommand = new DelegateCommand(BanUsername);
            BootPlayerCommand = new DelegateCommand(BootPlayer);
            SendMessageCommand = new DelegateCommand(SendMessage);
            SaveSettingsCommand = new DelegateCommand(SaveSettings);
            ToggleServerCommand = new DelegateCommand(ToggleServer);
        }

        private void ProcessServerActionsQueue(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            while (!_actionQueue.IsEmpty)
            {
                IServerAction action;
                if (_actionQueue.TryDequeue(out action))
                {
                    if (action.GetType() == typeof(PlayerConnectedAction))
                    {
                        var convertedAction = (PlayerConnectedAction) action;
                        if (!Players.Contains(convertedAction.Username))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Players.Add(convertedAction.Username);
                            });
                        }
                    }
                    else if (action.GetType() == typeof(PlayerDisconnectedAction))
                    {
                        var convertedAction = (PlayerDisconnectedAction) action;
                        if (Players.Contains(convertedAction.Username))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Players.Remove(convertedAction.Username);
                            });
                        }
                    }
                    else
                    {
                        action.Process();
                    }
                }
            }

            _serverGame.RefreshSettings(Settings);
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

        private string _startStopServerButtonText;

        public string StartStopServerButtonText
        {
            get { return _startStopServerButtonText; }
            set { SetProperty(ref _startStopServerButtonText, value); }
        }

        private bool _isServerRunning;
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
            if (_isServerRunning)
            {
                _serverGame.Exit();
            }
            else
            {
                try
                {
                    StartStopServerButtonText = "Stop Server";
                    StartServerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    StartStopServerButtonText = "Start Server";
                    _isServerRunning = false;
                    _queueTimer.Enabled = false;
                    Players.Clear();
                }
            }
            
        }

        private async void StartServerAsync()
        {
            await Task.Run(() =>
            {
                _isServerRunning = true;
                _queueTimer.Enabled = true;
                using (_serverGame = new ServerGame(this, _settings))
                {
                    _serverGame.Run();
                }
                StartStopServerButtonText = "Start Server";
                _queueTimer.Enabled = false;
                _isServerRunning = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Players.Clear();
                });
            });
        }

        public void QueueAction(IServerAction action)
        {
            _actionQueue.Enqueue(action);
        }

    }
}
