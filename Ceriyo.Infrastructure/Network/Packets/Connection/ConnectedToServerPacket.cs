﻿using System.Collections.Generic;
using Ceriyo.Core.Constants;
using Ceriyo.Infrastructure.Network.TransferObjects;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets.Connection
{
    [ProtoContract]
    public class ConnectedToServerPacket: PacketBase
    {
        [ProtoMember(1)]
        public string ServerName { get; set; }
        [ProtoMember(2)]
        public string Announcement { get; set; }
        [ProtoMember(3)]
        public GameCategory Category { get; set; }
        [ProtoMember(4)]
        public PVPType PVP { get; set; }
        [ProtoMember(5)]
        public int CurrentPlayers { get; set; }
        [ProtoMember(6)]
        public int MaxPlayers { get; set; }
        [ProtoMember(7)]
        public bool AllowCharacterDeletion { get; set; }
        [ProtoMember(8)]
        public List<string> RequiredResourcePacks { get; set; }
        [ProtoMember(9)]
        public List<PCTransferObject> PCs { get; set; }

        public ConnectedToServerPacket()
        {
            RequiredResourcePacks = new List<string>();
            PCs = new List<PCTransferObject>();
        }

        public override void Process()
        {
            
        }
    }
}
