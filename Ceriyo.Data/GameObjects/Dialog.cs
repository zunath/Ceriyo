using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.DataObjects;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.GameObjects
{
    public class Dialog : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.DialogsDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventType, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Dialog"; } }

        public Dialog()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventType, string>();
        }

        public Dialog(string name, string tag, string resref, string description = "", string comments = "")
        {
            Name = name;
            Tag = tag;
            Resref = resref;
            Description = description;
            Comments = comments;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventType, string>();
        }

    }
}
