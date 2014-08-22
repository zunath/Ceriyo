using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.Settings
{
    public class ServerSettings
    {
        public string ServerName { get; set; }
        public int Port { get; set; }
        public PVPTypeEnum PVP { get; set; }
        public int MaxPlayers { get; set; }
        public int MaxLevel { get; set; }
        public bool AllowCharacterDeletion { get; set; }
        public bool AllowFileAutoDownload { get; set; }
        public string PlayerPassword { get; set; }
        public string GMPassword { get; set; }
        public GameTypeEnum GameType { get; set; }
        public string Description { get; set; }
        public string Announcement { get; set; }
        public BindingList<string> Blacklist { get; set; }

        public ServerSettings()
        {
            ServerName = string.Empty;
            Port = 5121;
            PVP = PVPTypeEnum.None;
            MaxPlayers = 20;
            MaxLevel = EngineConstants.MaxLevel;
            AllowCharacterDeletion = true;
            AllowFileAutoDownload = false;
            PlayerPassword = string.Empty;
            GMPassword = string.Empty;
            GameType = GameTypeEnum.Action;
            Description = string.Empty;
            Announcement = string.Empty;
            Blacklist = new BindingList<string>();
        }
    }
}
