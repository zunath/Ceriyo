using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;

namespace Ceriyo.Data.GameObjects
{
    public class Area : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int LayerCount { get; set; }
        public string WorkingDirectory { get { return WorkingPaths.AreasDirectory; } }
        public List<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        public List<MapTile> MapTiles { get; set; }

        [XmlIgnore]
        public List<Tile[,]> Tiles
        {
            get
            {
                List<Tile[,]> tilesList = new List<Tile[,]>();

                for (int layer = 0; layer < LayerCount; layer++)
                {
                    Tile[,] tileLayer = new Tile[MapWidth, MapHeight];
                    for (int x = 0; x < MapWidth; x++)
                    {
                        for (int y = 0; y < MapHeight; y++)
                        {
                            tileLayer[x, y] = new Tile();
                        }
                    }

                    tilesList.Add(tileLayer);
                }

                return tilesList;
            }
        }

        public Area()
        {
        }

        public Area(
            string name,
            string tag,
            string resref,
            int tilesWide, 
            int tilesHigh, 
            int numberOfLayers)
        {
            this.Name = name;
            this.Tag = tag;
            this.Resref = resref;
            this.MapWidth = tilesWide;
            this.MapHeight = tilesHigh;
            this.LayerCount = numberOfLayers;
            this.MapTiles = new List<MapTile>();
            this.LocalVariables = new List<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();

            for (int layer = 0; layer < LayerCount; layer++)
            {
                for (int x = 0; x <= MapWidth; x++)
                {
                    for (int y = 0; y < MapHeight; y++)
                    {
                        MapTile tile = new MapTile(x, y, layer);
                        MapTiles.Add(tile);
                    }
                }
            }
        }
    }
}
