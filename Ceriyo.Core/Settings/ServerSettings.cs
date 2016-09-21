using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Settings
{
    [FilePath("./Settings/Server.json")]
    public class ServerSettings: INotifyPropertyChanged
    {
        private string _serverName;

        public string ServerName
        {
            get { return _serverName; }
            set
            {
                _serverName = value;
                OnPropertyChanged();
            }
        }

        private int _port;

        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged();
            }
        }

        private PVPType _pvpType;

        public PVPType PVPType
        {
            get { return _pvpType; }
            set
            {
                _pvpType = value;
                OnPropertyChanged();
            }
        }

        private int _maxPlayers;

        public int MaxPlayers
        {
            get { return _maxPlayers; }
            set
            {
                _maxPlayers = value;
                OnPropertyChanged();
            }
        }

        private bool _allowCharacterDeletion;
        public bool AllowCharacterDeletion
        {
            get { return _allowCharacterDeletion; }
            set
            {
                _allowCharacterDeletion = value;
                OnPropertyChanged();
            }
        }

        private bool _allowFileDownloading;

        public bool AllowFileDownloading
        {
            get { return _allowFileDownloading; }
            set
            {
                _allowFileDownloading = value;
                OnPropertyChanged();
            }
        }

        private string _playerPassword;

        public string PlayerPassword
        {
            get { return _playerPassword; }
            set
            {
                _playerPassword = value;
                OnPropertyChanged();
            }
        }

        private string _gmPassword;

        public string GMPassword
        {
            get { return _gmPassword; }
            set
            {
                _gmPassword = value;
                OnPropertyChanged();
            }
        }

        private GameCategory _gameCategory;
        public GameCategory GameCategory
        {
            get { return _gameCategory; }
            set
            {
                _gameCategory = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _announcement;

        public string Announcement
        {
            get { return _announcement; }
            set
            {
                _announcement = value;
                OnPropertyChanged();
            }
        }

        private BindingList<string> _blacklist;

        public BindingList<string> Blacklist
        {
            get { return _blacklist; }
            set
            {
                _blacklist = value;
                OnPropertyChanged();
            }
        }

        public ServerSettings()
        {
            ServerName = "Server";
            Port = 5121;
            PVPType = PVPType.None;
            MaxPlayers = 20;
            AllowCharacterDeletion = true;
            AllowFileDownloading = false;
            PlayerPassword = string.Empty;
            GMPassword = string.Empty;
            GameCategory = GameCategory.Action;
            Description = string.Empty;
            Announcement = string.Empty;
            Blacklist = new BindingList<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
