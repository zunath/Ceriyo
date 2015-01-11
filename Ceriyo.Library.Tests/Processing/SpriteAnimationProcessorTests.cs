using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.Processing;
using FlatRedBall.Graphics.Animation;
using Ceriyo.Data.Engine;

namespace Ceriyo.Library.Tests.Processing
{
    [TestClass]
    public class SpriteAnimationProcessorTests
    {
        [TestMethod]
        public void ToAnimationChain_CheckCoordinates()
        {
            const int testX = 20;
            const int testY = 20;

            SpriteAnimation animation = new SpriteAnimation();
            SpriteAnimationFrame frame = new SpriteAnimationFrame();
            frame.TextureCellX = testX;
            frame.TextureCellY = testY;

            animation.Frames.Add(frame);

            AnimationChain chain = SpriteAnimationProcessor.ToAnimationChain(animation);

            int expectedStartX = testX * EngineConstants.AnimationFrameWidth;
            int expectedStartY = testY * EngineConstants.AnimationFrameHeight;
            int expectedEndX = testX * EngineConstants.AnimationFrameWidth + EngineConstants.AnimationFrameWidth;
            int expectedEndY = testY * EngineConstants.AnimationFrameHeight + EngineConstants.AnimationFrameHeight;

            Assert.AreEqual(expectedStartX, chain[0].LeftCoordinate);
            Assert.AreEqual(expectedEndX, chain[0].RightCoordinate);
            Assert.AreEqual(expectedStartY, chain[0].TopCoordinate);
            Assert.AreEqual(expectedEndY, chain[0].BottomCoordinate);

        }

        [TestMethod]
        public void ToAnimationChain_FlipHorizontal_EqualToTrue()
        {
            SpriteAnimation animation = new SpriteAnimation();
            SpriteAnimationFrame frame = new SpriteAnimationFrame();
            frame.FlipHorizontal = true;
            animation.Frames.Add(frame);

            AnimationChain chain = SpriteAnimationProcessor.ToAnimationChain(animation);

            Assert.AreEqual(true, chain[0].FlipHorizontal);
        }

        [TestMethod]
        public void ToAnimationChain_FlipVertical_EqualToTrue()
        {
            SpriteAnimation animation = new SpriteAnimation();
            SpriteAnimationFrame frame = new SpriteAnimationFrame();
            frame.FlipVertical = true;
            animation.Frames.Add(frame);

            AnimationChain chain = SpriteAnimationProcessor.ToAnimationChain(animation);

            Assert.AreEqual(true, chain[0].FlipVertical);
        }
    }
}
