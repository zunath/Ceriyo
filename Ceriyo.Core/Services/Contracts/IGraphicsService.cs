using Microsoft.Xna.Framework.Graphics;
using XnaGraphicsDeviceManager = Microsoft.Xna.Framework.GraphicsDeviceManager;

namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Graphics service used with MonoGame.
    /// </summary>
    public interface IGraphicsService
    {
        /// <summary>
        /// Initializes the graphics service.
        /// </summary>
        /// <param name="xnaGraphics">The MonoGame graphics device manager.</param>
        void Initialize(XnaGraphicsDeviceManager xnaGraphics);

        /// <summary>
        /// The MonoGame graphics device.
        /// </summary>
        GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// The aspect ratio.
        /// </summary>
        float AspectRatio { get; }

        /// <summary>
        /// The sprite batch used for processing.
        /// </summary>
        SpriteBatch SpriteBatch { get; }
    }
}
