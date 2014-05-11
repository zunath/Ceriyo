using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;

namespace Ceriyo.Data.GameObjects
{
    public class Placeable : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string WorkingDirectory { get { return WorkingPaths.PlaceablesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        public string CategoryName { get { return "Placeable"; } }

        public Placeable()
        {
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Description = "";
            this.Comments = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
        }
    }
}
