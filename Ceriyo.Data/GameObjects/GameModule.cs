using Ceriyo.Data.Enumerations;
using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.DataObjects;

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
        public SerializableDictionary<ScriptEventType, string> Scripts { get; set; }
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
            Scripts = new SerializableDictionary<ScriptEventType, string>();
            Levels = new LevelChart();
            ResourcePacks = new BindingList<string>();

            AddEventStubs();
        }

        public GameModule(string name, string tag, string resref, string description = "", string comments = "")
        {
            Name = name;
            Tag = tag;
            Resref = resref;
            Description = description;
            Comments = comments;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventType, string>();
            Levels = new LevelChart();
            ResourcePacks = new BindingList<string>();

            AddEventStubs();
        }


        private void AddEventStubs()
        {
            Scripts.Add(ScriptEventType.OnAreaHeartbeat, string.Empty);
            Scripts.Add(ScriptEventType.OnModuleLoad, string.Empty);
            Scripts.Add(ScriptEventType.OnPlayerDeath, string.Empty);
            Scripts.Add(ScriptEventType.OnPlayerDying, string.Empty);
            Scripts.Add(ScriptEventType.OnModulePlayerEnter, string.Empty);
            Scripts.Add(ScriptEventType.OnModulePlayerLeaving, string.Empty);
            Scripts.Add(ScriptEventType.OnModulePlayerLeft, string.Empty);
            Scripts.Add(ScriptEventType.OnPlayerRespawn, string.Empty);

        }
    }
}
