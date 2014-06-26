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
        protected Sprite EntitySprite { get; set; }

        private AnimationChainList _animationChains;
        protected AnimationChainList AnimationChains 
        {
            get { return _animationChains; }
            set 
            {
                _animationChains = value;
                EntitySprite.AnimationChains = value;
            }
        }

        public GraphicEntity(string contentManagerName)
            : base(contentManagerName)
        {
            this.EntitySprite = new Sprite();
            this.EntitySprite.Visible = true;
            this.AnimationChains = new AnimationChainList();

            EntitySprite.PixelSize = 0.5f;
        }

    }
}
