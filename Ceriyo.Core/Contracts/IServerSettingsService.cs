using System.Collections.Concurrent;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Settings;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Tracks server settings for use across the server UI and server logic threads.
    /// </summary>
    public interface IServerSettingsService
    {
        /// <summary>
        /// The name of the server.
        /// </summary>
        string ServerName { get; }

        /// <summary>
        /// The port the server is running on.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// The PVP setting of the server.
        /// </summary>
        PVPType PVPType { get;  }

        /// <summary>
        /// The max number of players allowed to connect to the server at one time.
        /// </summary>
        int MaxPlayers { get; }

        /// <summary>
        /// Whether or not characters can be deleted.
        /// </summary>
        bool AllowCharacterDeletion { get; }

        /// <summary>
        /// Whether or not custom resources for the server can be downloaded by clients.
        /// </summary>
        bool AllowFileDownloading { get; }

        /// <summary>
        /// The password required by players to connect to the server.
        /// </summary>
        string PlayerPassword { get; }

        /// <summary>
        /// The password required by game masters to connect to the server.
        /// </summary>
        string GMPassword { get; }

        /// <summary>
        /// The game category of the server.
        /// </summary>
        GameCategory GameCategory { get; }

        /// <summary>
        /// The description of the server.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The announcement of the server.
        /// </summary>
        string Announcement { get; }

        /// <summary>
        /// The players who have been banned by this server.
        /// </summary>
        ConcurrentBag<string> BlackList { get; }

        /// <summary>
        /// Copies server settings from a ServerSettings object. This is done for thread safety.
        /// </summary>
        /// <param name="settings">The settings to copy.</param>
        void CopySettings(ServerSettings settings);

    }
}
