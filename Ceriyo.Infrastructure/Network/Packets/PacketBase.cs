﻿using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.TransferObjects;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets
{
    [ProtoContract]
    [ProtoInclude(1, typeof(ConnectionRequestPacket))]
    [ProtoInclude(2, typeof(ConnectedToServerPacket))]
    [ProtoInclude(3, typeof(CharacterCreatedPacket))]
    [ProtoInclude(4, typeof(CreateCharacterPacket))]
    public abstract class PacketBase : INetworkPacket
    {
        public abstract void Process();

    }
}
