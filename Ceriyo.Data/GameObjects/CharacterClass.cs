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
    public class CharacterClass : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.CharacterClassesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        [XmlIgnore]
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts 
        { 
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        public string CategoryName { get { return "CharacterClass"; } }

    }
}
