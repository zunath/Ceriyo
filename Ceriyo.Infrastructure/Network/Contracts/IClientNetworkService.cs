using System;
using Ceriyo.Core.Constants;
using Ceriyo.Infrastructure.Network.Packets;

namespace Ceriyo.Infrastructure.Network.Contracts
{
    public interface IClientNetworkService
    {
        void ConnectToServer(string ipAddress, int port, string username, string password);
        void DisconnectFromServer(string disconnectMessage = "");
        void ProcessMessages();
        void SendMessage(PacketDeliveryMethod method, INetworkPacket packet);
        string GetServerIPAddress();
        string BindPacketAction<T>(Action<PacketBase> action)
            where T: INetworkPacket;
        void UnbindPacketAction(string bindingID);

        event Action OnConnected;
        event Action OnDisconnected;
    }
}
