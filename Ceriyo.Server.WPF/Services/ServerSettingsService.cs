using System.Collections.Concurrent;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Settings;
using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Services
{
    /// <summary>
    /// This class is responsible for maintaining settings
    /// information for the server logic thread.
    /// </summary>
    public class ServerSettingsService: IServerSettingsService
    {
        public ServerSettingsService()
        {
            BlackList = new ConcurrentBag<string>();
        }

        public void CopySettings(ServerSettings settings)
        {
            ServerName = settings.ServerName;
            Port = settings.Port;
            PVPType = settings.PVPType;
            MaxPlayers = settings.MaxPlayers;
            AllowCharacterDeletion = settings.AllowCharacterDeletion;
            AllowFileDownloading = settings.AllowFileDownloading;
            PlayerPassword = settings.PlayerPassword;
            GMPassword = settings.GMPassword;
            GameCategory = settings.GameCategory;
            Description = settings.Description;
            Announcement = settings.Announcement;

            object lockObj = new object();
            lock (lockObj)
            {
                BlackList = new ConcurrentBag<string>();
            }

            foreach (var username in settings.Blacklist)
            {
                BlackList.Add(username);
            }
        }

        public string ServerName { get; private set; }
        public int Port { get; private set; }
        public PVPType PVPType { get; private set; }
        public int MaxPlayers { get; private set; }
        public bool AllowCharacterDeletion { get; private set; }
        public bool AllowFileDownloading { get; private set; }
        public string PlayerPassword { get; private set; }
        public string GMPassword { get; private set; }
        public GameCategory GameCategory { get; private set; }
        public string Description { get; private set; }
        public string Announcement { get; private set; }
        public ConcurrentBag<string> BlackList { get; private set; }
    }
}
