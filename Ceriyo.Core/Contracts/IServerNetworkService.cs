using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Contracts
{
    public interface IServerNetworkService
    {
        void StartServer(int port);
        void StopServer();
        void ProcessMessages();
        void SendMessage(PacketDeliveryMethod method, INetworkPacket packet, string accountName);
    }
}
