using Microsoft.Xna.Framework.Graphics;
using XnaGraphicsDeviceManager = Microsoft.Xna.Framework.GraphicsDeviceManager;

namespace Ceriyo.Core.Contracts
{
    public interface IGraphicsService
    {
        void Initialize(XnaGraphicsDeviceManager xnaGraphics);
        GraphicsDevice GraphicsDevice { get; }
        float AspectRatio { get; }
        SpriteBatch SpriteBatch { get; }
    }
}
