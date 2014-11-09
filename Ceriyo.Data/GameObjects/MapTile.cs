using System.Xml.Serialization;
using FlatRedBall;
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
        public int TileDefinitionX { get; set; }
        [ProtoMember(5)]
        public int TileDefinitionY { get; set; }
        [ProtoMember(6)]
        public bool HasGraphic { get; set; }

        [XmlIgnore]
        public Sprite TileSprite { get; set; }

        public MapTile()
        {
            MapX = 0;
            MapY = 0;
            Layer = 0;
            TileDefinitionX = 0;
            TileDefinitionY = 0;
            HasGraphic = false;
            TileSprite = new Sprite();
        }

        public MapTile(int mapX, int mapY, int layer)
        {
            MapX = mapX;
            MapY = mapY;
            Layer = layer;
            TileDefinitionX = 0;
            TileDefinitionY = 0;
            HasGraphic = false;
            TileSprite = new Sprite();
        }
    }
}
