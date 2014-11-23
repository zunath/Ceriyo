using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Ceriyo.Data.GameObjects
{
    public class GameModule : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { throw new NotSupportedException(); } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Module"; } }
        public LevelChart Levels { get; set; }
        public BindingList<string> ResourcePacks { get; set; }

        public GameModule()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            Levels = new LevelChart();
            ResourcePacks = new BindingList<string>();
        }

        public GameModule(string name, string tag, string resref, string description = "", string comments = "")
        {
            Name = name;
            Tag = tag;
            Resref = resref;
            Description = description;
            Comments = comments;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            Levels = new LevelChart();
            ResourcePacks = new BindingList<string>();
        }
    }
}
