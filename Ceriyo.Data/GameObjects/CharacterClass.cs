using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using ProtoBuf;

namespace Ceriyo.Data.GameObjects
{
    [ProtoContract]
    public class CharacterClass : IGameObject
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Tag { get; set; }
        [ProtoMember(3)]
        public string Resref { get; set; }
        [ProtoMember(4)]
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
