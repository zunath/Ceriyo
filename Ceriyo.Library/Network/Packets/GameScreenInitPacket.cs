using System.ComponentModel;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    public class GameScreenInitPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public string AreaName { get; set; }
        [ProtoMember(3)]
        public string AreaTag { get; set; }
        [ProtoMember(4)]
        public string AreaResref { get; set; }
        [ProtoMember(5)]
        public string AreaDescription { get; set; }
        [ProtoMember(6)]
        public BindingList<MapTile> AreaTiles { get; set; }
        [ProtoMember(7)]
        public int AreaLayers { get; set; }
        [ProtoMember(8)]
        public string TilesetGraphicResourcePackage { get; set; }
        [ProtoMember(9)]
        public string TilesetGraphicResourceFileName { get; set; }

        public GameScreenInitPacket()
        {
            IsRequest = false;
            AreaName = string.Empty;
            AreaTag = string.Empty;
            AreaResref = string.Empty;
            AreaDescription = string.Empty;
            AreaTiles = new BindingList<MapTile>();
        }
    }
}
