using System;
using Ceriyo.Core.Constants;
using Ceriyo.Infrastructure.Network.Packets;

namespace Ceriyo.Infrastructure.Network.Contracts
{
    public interface IServerNetworkService
    {
        void StartServer(int port);
        void StopServer();
        void ProcessMessages();
        void SendMessage(PacketDeliveryMethod method, INetworkPacket packet, string accountName);

        void BootUsername(string username);

        string BindPacketAction<T>(Action<string, PacketBase> action)
            where T : INetworkPacket;
        void UnbindPacketAction(string bindingID);

        event Action<string> OnPlayerConnected;
        event Action<string> OnPlayerDisconnected;
    }
}
