using System.Collections.Generic;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Lidgren.Network;

namespace Ceriyo.Library.Network
{
    public class NetworkTransferData
    {
        public Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        public ServerSettings Settings { get; set; }

        public NetworkTransferData()
        {
        }
    }
}
