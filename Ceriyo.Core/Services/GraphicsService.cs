using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework.Graphics;
using XnaGraphicsDeviceManager = Microsoft.Xna.Framework.GraphicsDeviceManager;

namespace Ceriyo.Core.Services
{
    public class GraphicsService: IGraphicsService
    {
        private XnaGraphicsDeviceManager _graphics;

        // Initialize all graphics properties.
        public void Initialize(XnaGraphicsDeviceManager xnaGraphics)
        {
            if (null != _graphics)
            {
                return;
            }

            _graphics = xnaGraphics;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            GraphicsDevice = _graphics.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public GraphicsDevice GraphicsDevice { get; private set; }
        public float AspectRatio => GraphicsDevice.Viewport.AspectRatio;
        public SpriteBatch SpriteBatch { get; private set; }
    }
}
