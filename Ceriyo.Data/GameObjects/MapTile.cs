using ProtoBuf;

namespace Ceriyo.Data.GameObjects
{
    [ProtoContract]
    public class MapTile
    {
        [ProtoMember(1)]
        public int MapX { get; set; }
        [ProtoMember(2)]
        public int MapY { get; set; }
        [ProtoMember(3)]
        public int Layer { get; set; }
        [ProtoMember(4)]
        public bool HasGraphic { get; set; }
        [ProtoMember(5)]
        public int TileSheetX { get; set; }
        [ProtoMember(6)]
        public int TileSheetY { get; set; }

        public MapTile()
        {
            MapX = 0;
            MapY = 0;
            TileSheetX = 0;
            TileSheetY = 0;
            Layer = 0;
            HasGraphic = false;
        }

        public MapTile(int mapX, int mapY, int layer)
        {
            MapX = mapX;
            MapY = mapY;
            Layer = layer;
            HasGraphic = false;
        }
    }
}
