using System.IO;
using System.Linq;
using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Ceriyo.Network;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserInfoPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public string Username { get; set; }
        [ProtoMember(3)]
        public string ServerPassword { get; set; }

        public UserInfoPacket()
        {
            IsRequest = false;
            Username = string.Empty;
            ServerPassword = string.Empty;
        }

        public override ServerNetworkData Receive(ServerNetworkData data)
        {
            if (!data.Players.ContainsKey(SenderConnection) &&
                data.Players.SingleOrDefault(x => x.Value.Username == Username).Value == null)
            {
                ServerPlayer pc = new ServerPlayer
                {
                    PC = new Player(),
                    Username = Username
                };

                data.Players.Add(SenderConnection, pc);

                if (!Directory.Exists(EnginePaths.CharactersDirectory + Username))
                {
                    Directory.CreateDirectory(EnginePaths.CharactersDirectory + Username);
                }

                UserConnectedPacket response = new UserConnectedPacket
                {
                    IsSuccessful = true
                };

                data.ResponsePacket = response;
                data.DeliveryMethod = NetDeliveryMethod.ReliableUnordered;
            }

            return data;
        }

        public override ServerNetworkData Send(ServerNetworkData data)
        {

            return data;
        }
    }
}
