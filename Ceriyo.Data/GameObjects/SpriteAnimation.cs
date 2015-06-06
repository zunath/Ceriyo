using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.DataObjects;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.GameObjects
{
    public class SpriteAnimation : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public BindingList<SpriteAnimationFrame> Frames { get; set; }
        public GameResource Graphic { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.AnimationsDirectory; } }
        [XmlIgnore]
        public string CategoryName { get { return "Animation"; } }

        [XmlIgnore]
        public BindingList<LocalVariable> LocalVariables
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        [XmlIgnore]
        public SerializableDictionary<ScriptEventType, string> Scripts
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public SpriteAnimation()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            Frames = new BindingList<SpriteAnimationFrame>();
            Graphic = new GameResource();
        }
    }
}
