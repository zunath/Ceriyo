using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ceriyo.Data.GameObjects
{
    public class ItemType : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.ItemTypesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "ItemTypes"; } }
        public int StackSize { get; set; }
        public BindingList<InventorySlotEnum> WearableInventorySlots { get; set; }

        public ItemType()
        {
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Description = "";
            this.Comments = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.StackSize = 1;
            this.WearableInventorySlots = new BindingList<InventorySlotEnum>();
        }
    }
}
