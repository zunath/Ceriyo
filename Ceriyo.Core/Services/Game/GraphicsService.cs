using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework.Graphics;
using XnaGraphicsDeviceManager = Microsoft.Xna.Framework.GraphicsDeviceManager;

namespace Ceriyo.Core.Services.Game
{
    /// <inheritdoc />
    public class GraphicsService: IGraphicsService
    {
        private XnaGraphicsDeviceManager _graphics;

        /// <inheritdoc />
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

        /// <inheritdoc />
        public GraphicsDevice GraphicsDevice { get; private set; }

        /// <inheritdoc />
        public float AspectRatio => GraphicsDevice.Viewport.AspectRatio;

        /// <inheritdoc />
        public SpriteBatch SpriteBatch { get; private set; }
    }
}
