using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using Ceriyo.Data.ResourceObjects;
using ProtoBuf;

namespace Ceriyo.Data.GameObjects
{
    [ProtoContract]
    public class Item : IGameObject
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Tag { get; set; }
        [ProtoMember(3)]
        public string Resref { get; set; }
        [ProtoMember(4)]
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.ItemsDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Item"; } }

        public string ItemTypeResref { get; set; }
        [XmlIgnore]
        public ItemType ItemType 
        {
            get
            {
                return WorkingDataManager.GetGameObject<ItemType>(ModulePaths.ItemTypesDirectory, ItemTypeResref);
            }
        }
        public int Price { get; set; }
        public GameResource InventoryGraphic { get; set; }
        public GameResource WorldGraphic { get; set; }

        public bool IsStolen { get; set; }
        public bool IsPlot { get; set; }
        public bool IsUndroppable { get; set; }
        public BindingList<AssignedItemProperty> AssignedItemProperties { get; set; }


        public BindingList<ItemClassRequirement> ItemRequirements { get; set; }

        public Item()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            ItemTypeResref = string.Empty;
            Price = 0;
            IsStolen = false;
            IsPlot = false;
            IsUndroppable = false;
            InventoryGraphic = new GameResource();
            WorldGraphic = new GameResource();
            AssignedItemProperties = new BindingList<AssignedItemProperty>();
            ItemRequirements = new BindingList<ItemClassRequirement>();
            
            Scripts.Add(ScriptEventTypeEnum.OnItemAcquired, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnItemActivated, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnItemEquipped, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnItemUnacquired, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnItemUnequipped, string.Empty);
        }
    }
}
