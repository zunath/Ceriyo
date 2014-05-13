using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using Ceriyo.Data.ResourceObjects;

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
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.AreasDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        public BindingList<MapTile> MapTiles { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Area"; } }
        public BindingList<Creature> CreatureInstances { get; set; }
        public BindingList<Placeable> PlaceableInstances { get; set; }
        public BindingList<Item> ItemInstances { get; set; }
        public GameResource Graphic { get; set; }
        public GameResource BattleMusic { get; set; }
        public GameResource BackgroundMusic { get; set; }

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
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Description = "";
            this.Comments = "";
            this.MapWidth = EngineConstants.AreaMaxWidth;
            this.MapHeight = EngineConstants.AreaMaxHeight;
            this.LayerCount = EngineConstants.AreaMaxLayers;
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.MapTiles = new BindingList<MapTile>();
            this.Graphic = new GameResource();
            this.BattleMusic = new GameResource();
            this.BackgroundMusic = new GameResource();
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
            this.MapTiles = new BindingList<MapTile>();
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.CreatureInstances = new BindingList<Creature>();
            this.ItemInstances = new BindingList<Item>();
            this.PlaceableInstances = new BindingList<Placeable>();
            this.Graphic = new GameResource();
            this.BattleMusic = new GameResource();
            this.BackgroundMusic = new GameResource();

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
