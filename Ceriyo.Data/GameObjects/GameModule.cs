using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;

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
        public string CategoryName { get { return "Module"; } }
        public LevelChart Levels { get; set; }

        public GameModule()
        {
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Description = "";
            this.Comments = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.Levels = new LevelChart();
        }

        public GameModule(string name, string tag, string resref, string description = "", string comments = "")
        {
            this.Name = name;
            this.Tag = tag;
            this.Resref = resref;
            this.Description = description;
            this.Comments = comments;
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.Levels = new LevelChart();
        }

        public string ListBoxName
        {
            get
            {
                return Name + " (" + Resref + EnginePaths.ModuleExtension + ")";
            }
        }
    }
}
