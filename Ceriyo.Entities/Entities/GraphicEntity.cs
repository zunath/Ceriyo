using Ceriyo.Data.ResourceObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Entities.Entities
{
    public abstract class GraphicEntity : BaseEntity
    {
        private GameResourceProcessor Processor { get; set; }
        protected GameResource GraphicResource { get; set; }
        protected Sprite EntitySprite { get; set; }
        
        public GraphicEntity(string contentManagerName, GameResource graphic)
            : base(contentManagerName)
        {
            this.GraphicResource = graphic;
            this.EntitySprite = new Sprite();
            this.EntitySprite.Visible = true;
            
        }

    }
}
