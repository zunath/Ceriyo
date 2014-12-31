using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using Ceriyo.Data.Engine;
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
        
        [XmlIgnore]
        public int MapWidth 
        {
            get
            {
                if (MapTiles == null || MapTiles.Count <= 0)
                {
                    return 0;
                }
                return MapTiles.Max(x => x.MapX) + 1;
            }
        }

        [XmlIgnore]
        public int MapHeight 
        {
            get
            {
                if (MapTiles == null || MapTiles.Count <= 0)
                {
                    return 0;
                }
                return MapTiles.Max(y => y.MapY) + 1;
            }
        }
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
                    WorkingDataManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory)
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
                    WorkingDataManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory)
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
                    WorkingDataManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory)
                                      .Where(x => ItemInstancesResrefs.Contains(x.Resref))
                                      .ToList());
            }
        }

        [XmlIgnore]
        public Tileset AreaTileset
        {
            get
            {
                return WorkingDataManager.GetGameObject<Tileset>(ModulePaths.TilesetsDirectory, AreaTilesetResref);
            }
        }

        public string AreaTilesetResref { get; set; }
        public GameResource BattleMusic { get; set; }
        public GameResource BackgroundMusic { get; set; }

        public Area()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LayerCount = EngineConstants.AreaMaxLayers;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            MapTiles = new BindingList<MapTile>();
            AreaTilesetResref = string.Empty;
            BattleMusic = new GameResource();
            BackgroundMusic = new GameResource();
        }

        public Area(
            string name,
            string tag,
            string resref,
            int tilesWide, 
            int tilesHigh, 
            int numberOfLayers)
        {
            Name = name;
            Tag = tag;
            Resref = resref;
            LayerCount = numberOfLayers;
            MapTiles = new BindingList<MapTile>();
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            CreatureInstancesResrefs = new BindingList<string>();
            ItemInstancesResrefs = new BindingList<string>();
            PlaceableInstancesResrefs = new BindingList<string>();
            AreaTilesetResref = string.Empty;
            BattleMusic = new GameResource();
            BackgroundMusic = new GameResource();

            for (int layer = 0; layer < LayerCount; layer++)
            {
                for (int x = 0; x < tilesWide; x++)
                {
                    for (int y = 0; y < tilesHigh; y++)
                    {
                        MapTile tile = new MapTile(x, y, layer);
                        MapTiles.Add(tile);
                    }
                }
            }
        }
    }
}
