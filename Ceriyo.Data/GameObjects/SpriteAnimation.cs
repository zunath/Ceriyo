using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.IO;

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
            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Resref = string.Empty;
            this.Description = string.Empty;
            this.Comments = string.Empty;
            this.Frames = new BindingList<SpriteAnimationFrame>();
            this.Graphic = new GameResource();
        }
    }
}
