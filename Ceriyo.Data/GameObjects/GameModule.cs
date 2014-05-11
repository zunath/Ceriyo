using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string WorkingDirectory { get { throw new NotSupportedException(); } }
        public List<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }

        public GameModule()
        { 
        }

        public GameModule(string name, string tag, string resref)
        {
            this.Name = name;
            this.Tag = tag;
            this.Resref = resref;
        }

        public string ListBoxName
        {
            get
            {
                return Name + " (" + Resref + ModulePaths.ModuleExtension + ")";
            }
        }
    }
}
