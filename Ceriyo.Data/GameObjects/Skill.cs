using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using ProtoBuf;

namespace Ceriyo.Data.GameObjects
{
    [ProtoContract]
    public class Skill : IGameObject
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
        public string WorkingDirectory { get { return WorkingPaths.SkillsDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Skill"; } }

        public Skill()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;

            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
        }
    }
}
