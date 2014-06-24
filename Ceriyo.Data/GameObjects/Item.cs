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
    public class Item : IGameObject
    {
        [XmlIgnore]
        private WorkingDataManager WorkingManager { get; set; }

        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
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
                return WorkingManager.GetGameObject<ItemType>(ModulePaths.ItemTypesDirectory, ItemTypeResref);
            }
        }
        public int Price { get; set; }
        public GameResource InventoryGraphic { get; set; }
        public GameResource WorldGraphic { get; set; }

        public bool IsStolen { get; set; }
        public bool IsPlot { get; set; }
        public bool IsUndroppable { get; set; }

        public BindingList<string> ItemPropertiesResrefs { get; set; }

        [XmlIgnore]
        public BindingList<ItemProperty> ItemProperties
        {
            get
            {
                return new BindingList<ItemProperty>(
                    WorkingManager.GetAllGameObjects<ItemProperty>(ModulePaths.ItemPropertiesDirectory)
                                      .Where(x =>  ItemPropertiesResrefs.Contains(x.Resref))
                                      .ToList());
            }

        }
        public BindingList<ItemClassRequirement> ItemRequirements { get; set; }

        public Item()
        {
            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Resref = string.Empty;
            this.Description = string.Empty;
            this.Comments = string.Empty;
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.ItemTypeResref = string.Empty;
            this.Price = 0;
            this.IsStolen = false;
            this.IsPlot = false;
            this.IsUndroppable = false;
            this.InventoryGraphic = new GameResource();
            this.WorldGraphic = new GameResource();
            this.ItemPropertiesResrefs = new BindingList<string>();
            this.ItemRequirements = new BindingList<ItemClassRequirement>();
            this.WorkingManager = new WorkingDataManager();

            Scripts.Add(ScriptEventTypeEnum.OnItemAcquired, "");
            Scripts.Add(ScriptEventTypeEnum.OnItemActivated, "");
            Scripts.Add(ScriptEventTypeEnum.OnItemEquipped, "");
            Scripts.Add(ScriptEventTypeEnum.OnItemUnacquired, "");
            Scripts.Add(ScriptEventTypeEnum.OnItemUnequipped, "");
        }
    }
}
