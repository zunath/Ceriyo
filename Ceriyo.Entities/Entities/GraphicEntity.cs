using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics.Animation;

namespace Ceriyo.Entities
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

        protected GraphicEntity(string contentManagerName)
            : base(contentManagerName)
        {
            EntitySprite = new Sprite
            {
                Visible = true
            };
            AnimationChains = new AnimationChainList();

            EntitySprite.PixelSize = 0.5f;
        }

    }
}
