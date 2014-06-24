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
        [XmlIgnore]
        private WorkingDataManager WorkingManager { get; set; }

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

        public BindingList<string> CreatureInstancesResrefs { get; set; }
        public BindingList<string> PlaceableInstancesResrefs { get; set; }
        public BindingList<string> ItemInstancesResrefs { get; set; }

        [XmlIgnore]
        public BindingList<Creature> CreatureInstances 
        {
            get
            {
                return new BindingList<Creature>(
                    WorkingManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory)
                                      .Where(x => CreatureInstancesResrefs.Contains(x.Resref))
                                      .ToList());
            }
        }
        [XmlIgnore]
        public BindingList<Placeable> PlaceableInstances 
        {
            get
            {
                return new BindingList<Placeable>(
                    WorkingManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory)
                                      .Where(x => PlaceableInstancesResrefs.Contains(x.Resref))
                                      .ToList());
            }
        }
        [XmlIgnore]
        public BindingList<Item> ItemInstances 
        {
            get
            {
                return new BindingList<Item>(
                    WorkingManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory)
                                      .Where(x => ItemInstancesResrefs.Contains(x.Resref))
                                      .ToList());
            }
        }
        public Tileset AreaTileset { get; set; }
        public GameResource BattleMusic { get; set; }
        public GameResource BackgroundMusic { get; set; }

        public Area()
        {
            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Resref = string.Empty;
            this.Description = string.Empty;
            this.Comments = string.Empty;
            this.MapWidth = EngineConstants.AreaMaxWidth;
            this.MapHeight = EngineConstants.AreaMaxHeight;
            this.LayerCount = EngineConstants.AreaMaxLayers;
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.MapTiles = new BindingList<MapTile>();
            this.AreaTileset = new Tileset();
            this.BattleMusic = new GameResource();
            this.BackgroundMusic = new GameResource();
            this.WorkingManager = new WorkingDataManager();
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
            this.CreatureInstancesResrefs = new BindingList<string>();
            this.ItemInstancesResrefs = new BindingList<string>();
            this.PlaceableInstancesResrefs = new BindingList<string>();
            this.AreaTileset = new Tileset();
            this.BattleMusic = new GameResource();
            this.BackgroundMusic = new GameResource();
            this.WorkingManager = new WorkingDataManager();

            for (int layer = 0; layer < LayerCount; layer++)
            {
                for (int x = 0; x < MapWidth; x++)
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
