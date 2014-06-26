using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using FlatRedBall.Graphics.Animation;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Library.Processing
{
    public class SpriteAnimationProcessor
    {

        public AnimationChain ToAnimationChain(SpriteAnimation animation)
        {
            AnimationChain chain = new AnimationChain();
            GameResourceProcessor processor = new GameResourceProcessor();
            Texture2D texture = processor.ToTexture2D(animation.Graphic);

            foreach (SpriteAnimationFrame spriteFrame in animation.Frames)
            {
                AnimationFrame frame = new AnimationFrame();
                frame.FrameLength = spriteFrame.FrameLength;
                frame.Texture = texture;
                frame.LeftCoordinate = spriteFrame.TextureCellX * EngineConstants.AnimationFrameWidth;
                frame.TopCoordinate = spriteFrame.TextureCellY * EngineConstants.AnimationFrameHeight;
                frame.RightCoordinate = spriteFrame.TextureCellX * EngineConstants.AnimationFrameWidth + EngineConstants.AnimationFrameWidth;
                frame.BottomCoordinate = spriteFrame.TextureCellY * EngineConstants.AnimationFrameHeight + EngineConstants.AnimationFrameHeight;
                frame.FlipHorizontal = spriteFrame.FlipHorizontal;
                frame.FlipVertical = spriteFrame.FlipVertical;

                chain.Add(frame);
            }

            return chain;
        }


        public SpriteAnimationProcessor()
        {
        }
    }
}
