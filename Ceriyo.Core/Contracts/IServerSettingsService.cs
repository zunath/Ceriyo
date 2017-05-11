using System.Collections.Concurrent;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Settings;

namespace Ceriyo.Core.Contracts
{
    public interface IServerSettingsService
    {
        string ServerName { get; }
        int Port { get; }
        PVPType PVPType { get;  }
        int MaxPlayers { get; }
        bool AllowCharacterDeletion { get; }

        bool AllowFileDownloading { get; }
        string PlayerPassword { get; }
        string GMPassword { get; }
        GameCategory GameCategory { get; }
        string Description { get; }
        string Announcement { get; }
        ConcurrentBag<string> BlackList { get; }


        void CopySettings(ServerSettings settings);

    }
}
