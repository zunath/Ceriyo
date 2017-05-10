using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Packets
{
    public class ConnectionRequestPacket: INetworkPacket
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public void Process()
        {
            
        }
    }
}
