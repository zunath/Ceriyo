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

        public ItemType ItemType { get; set; }
        public int Price { get; set; }
        public GameResource InventoryGraphic { get; set; }
        public GameResource WorldGraphic { get; set; }

        public bool IsStolen { get; set; }
        public bool IsPlot { get; set; }
        public bool IsUndroppable { get; set; }


        public Item()
        {
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Description = "";
            this.Comments = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.ItemType = new ItemType();
            this.Price = 0;
            this.IsStolen = false;
            this.IsPlot = false;
            this.IsUndroppable = false;
            this.InventoryGraphic = new GameResource();
            this.WorldGraphic = new GameResource();
        }
    }
}
