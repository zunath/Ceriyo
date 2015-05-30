using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.DataObjects;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.GameObjects
{
    public class ItemProperty : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.ItemPropertiesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventType, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "ItemProperties"; } }
        public BindingList<ItemPropertyOption> Options { get; set; }
        public ItemPropertyType ItemPropertyType { get; set; }

        public ItemProperty()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventType, string>();
            Options = new BindingList<ItemPropertyOption>();
            ItemPropertyType = ItemPropertyType.Unknown;
        }
    }
}
