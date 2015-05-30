﻿using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.DataObjects;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using ProtoBuf;

namespace Ceriyo.Data.GameObjects
{
    [ProtoContract]
    public class Ability : IGameObject
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
        [ProtoMember(5)]
        public bool IsPassive { get; set; }

        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.AbilitiesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventType, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Ability"; } }

        public Ability()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            IsPassive = true;

            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventType, string>();

            Scripts.Add(ScriptEventType.OnAbilityActivated, string.Empty);
        }
    }
}
