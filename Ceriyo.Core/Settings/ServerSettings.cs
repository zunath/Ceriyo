using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Settings
{
    /// <summary>
    /// Tracks server settings which are adjusted by the end user.
    /// </summary>
    [FilePath("./Settings/Server.settings")]
    public class ServerSettings: INotifyPropertyChanged
    {
        private string _serverName;

        /// <summary>
        /// The name of the server.
        /// </summary>
        public string ServerName
        {
            get => _serverName;
            set
            {
                _serverName = value;
                OnPropertyChanged();
            }
        }

        private int _port;

        /// <summary>
        /// The port used by the server.
        /// </summary>
        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyChanged();
            }
        }

        private PVPType _pvpType;

        /// <summary>
        /// The PVP setting for the server.
        /// </summary>
        public PVPType PVPType
        {
            get => _pvpType;
            set
            {
                _pvpType = value;
                OnPropertyChanged();
            }
        }

        private int _maxPlayers;

        /// <summary>
        /// The max number of players the server allows at one time.
        /// </summary>
        public int MaxPlayers
        {
            get => _maxPlayers;
            set
            {
                _maxPlayers = value;
                OnPropertyChanged();
            }
        }

        private bool _allowCharacterDeletion;

        /// <summary>
        /// Whether or not the server allows players to delete their own characters.
        /// </summary>
        public bool AllowCharacterDeletion
        {
            get => _allowCharacterDeletion;
            set
            {
                _allowCharacterDeletion = value;
                OnPropertyChanged();
            }
        }

        private bool _allowFileDownloading;

        /// <summary>
        /// Whether or not the server allows custom files to be downloaded.
        /// </summary>
        public bool AllowFileDownloading
        {
            get => _allowFileDownloading;
            set
            {
                _allowFileDownloading = value;
                OnPropertyChanged();
            }
        }

        private string _playerPassword;

        /// <summary>
        /// The password required for players to connect to the server.
        /// </summary>
        public string PlayerPassword
        {
            get => _playerPassword;
            set
            {
                _playerPassword = value;
                OnPropertyChanged();
            }
        }

        private string _gmPassword;

        /// <summary>
        /// The password required for game masters to connect to the server.
        /// </summary>
        public string GMPassword
        {
            get => _gmPassword;
            set
            {
                _gmPassword = value;
                OnPropertyChanged();
            }
        }

        private GameCategory _gameCategory;

        /// <summary>
        /// The category the server will be placed in on the master server.
        /// </summary>
        public GameCategory GameCategory
        {
            get => _gameCategory;
            set
            {
                _gameCategory = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        /// <summary>
        /// The public description of the server.
        /// </summary>
        public string Description {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _announcement;

        /// <summary>
        /// The public announcement shown on the server login.
        /// </summary>
        public string Announcement
        {
            get => _announcement;
            set
            {
                _announcement = value;
                OnPropertyChanged();
            }
        }

        private BindingList<string> _blacklist;

        /// <summary>
        /// The list of banned user accounts.
        /// </summary>
        public BindingList<string> Blacklist
        {
            get => _blacklist;
            set
            {
                _blacklist = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructs a new server settings object.
        /// </summary>
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

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
