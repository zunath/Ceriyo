using System.IO;
using System.Linq;
using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
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

        // Receiving from client
        public override NetworkTransferData ServerReceive(NetworkTransferData data)
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

                response.Send(NetDeliveryMethod.ReliableUnordered, SenderConnection);
            }

            return data;
        }

        public override NetworkTransferData ClientReceive(NetworkTransferData data)
        {
            UserInfoPacket response = new UserInfoPacket
            {
                IsRequest = false,
                Username = "zunath" // TODO: Store username after it's been authorized by the master server
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, SenderConnection);
            
            return data;
        }
    }
}
