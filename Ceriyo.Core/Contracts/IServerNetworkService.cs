using System;
using Ceriyo.Core.Constants;
using Ceriyo.Core.EventArgs;

namespace Ceriyo.Core.Contracts
{
    public interface IServerNetworkService
    {
        void StartServer(int port);
        void StopServer();
        void ProcessMessages();
        void SendMessage(PacketDeliveryMethod method, INetworkPacket packet, string accountName);

        event EventHandler<NetworkConnectionEventArgs> OnPlayerConnected;
        event EventHandler<NetworkConnectionEventArgs> OnPlayerDisconnected;

    }
}
