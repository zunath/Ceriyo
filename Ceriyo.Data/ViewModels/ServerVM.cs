using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Settings;

namespace Ceriyo.Data.ViewModels
{
    public class ServerVM : BaseVM
    {
        private ServerSettings _serverSettings;
        private string _serverMessage;
        private string _serverStatusMessage;
        private string _blacklistUsername;
        private string _moduleFileName;
        private int _moduleMaxLevel;
        private string _ipAddress;
        private BindingList<string> _connectedUsernames;
        private BindingList<string> _logMessages;
        private BindingList<string> _modules;
        private string _selectedModule;
        private bool _isServerRunning;

        public bool IsServerRunning
        {
            get
            {
                return _isServerRunning;
            }
            set
            {
                _isServerRunning = value;
                OnPropertyChanged("IsServerRunning");
            }
        }

        public string SelectedModule
        {
            get
            {
                return _selectedModule;
            }
            set
            {
                _selectedModule = value;
                OnPropertyChanged("SelectedModule");
            }
        }

        public BindingList<string> Modules
        {
            get
            {
                return _modules;
            }
            set
            {
                _modules = value;
                OnPropertyChanged("Modules");
            }
        }

        public BindingList<string> ConnectedUsernames
        {
            get
            {
                return _connectedUsernames;
            }
            set
            {
                _connectedUsernames = value;
                OnPropertyChanged("ConnectedUsernames");
            }
        }

        public BindingList<string> LogMessages
        {
            get
            {
                return _logMessages;
            }
            set
            {
                _logMessages = value;
                OnPropertyChanged("LogMessages");
            }
        }

        public string IPAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
                OnPropertyChanged("IPAddress");
            }

        }

        public int ModuleMaxLevel
        {
            get
            {
                return _moduleMaxLevel;
            }
            set
            {
                _moduleMaxLevel = value;
                OnPropertyChanged("ModuleMaxLevel");
            }
        }

        public ServerSettings ServerSettings 
        {
            get
            {
                return _serverSettings;
            }
            set
            {
                _serverSettings = value;
                OnPropertyChanged("ServerSettings");
            }
        }

        public string ServerMessage
        {
            get
            {
                return _serverMessage;
            }
            set
            {
                _serverMessage = value;
                OnPropertyChanged("ServerMessage");
            }
        }

        public string ServerStatusMessage
        {
            get
            {
                return _serverStatusMessage;
            }
            set
            {
                _serverStatusMessage = value;
                OnPropertyChanged("ServerStatusMessage");
            }
        }

        public string BlacklistUsername
        {
            get
            {
                return _blacklistUsername;
            }
            set
            {
                _blacklistUsername = value;
                OnPropertyChanged("BlacklistUsername");
            }
        }

        public string ModuleFileName
        {
            get
            {
                return _moduleFileName;
            }
            set
            {
                _moduleFileName = value;
                OnPropertyChanged("ModuleFileName");
            }
        }

        public ServerVM()
        {
            ServerSettings = new ServerSettings();
            ServerMessage = string.Empty;
            ServerStatusMessage = string.Empty;
            BlacklistUsername = string.Empty;
            ModuleFileName = string.Empty;
            ModuleMaxLevel = EngineConstants.MaxLevel;
            IPAddress = string.Empty;
            ConnectedUsernames = new BindingList<string>();
            LogMessages = new BindingList<string>();
            Modules = new BindingList<string>();
            SelectedModule = string.Empty;
            IsServerRunning = false;
        }
    }
}
