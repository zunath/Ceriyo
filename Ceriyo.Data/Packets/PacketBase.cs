using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    [ProtoInclude(100, typeof(UserConnectedPacket))]
    [ProtoInclude(101, typeof(UserDisconnectedPacket))]
    [ProtoInclude(102, typeof(UserInfoPacket))]
    [ProtoInclude(103, typeof(DeleteCharacterPacket))]
    [ProtoInclude(104, typeof(CharacterCreationScreenPacket))]
    [ProtoInclude(105, typeof(CreateCharacterPacket))]
    [ProtoInclude(106, typeof(CharacterSelectionScreenPacket))]
    public class PacketBase
    {
        public NetConnection SenderConnection { get; set; }

        public PacketBase()
        {
        }

    }
}
