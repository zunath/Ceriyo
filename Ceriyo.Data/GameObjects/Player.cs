using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using Lidgren.Network;

namespace Ceriyo.Data.GameObjects
{
    public class Player: IGameObject
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
        public string CategoryName { get { return "Player"; } }

        [XmlIgnore]
        public NetConnection Connection { get; set; }

        public Player()
        {
            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Resref = string.Empty;
            this.Description = string.Empty;
            this.Comments = string.Empty;
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
        }
    }
}
