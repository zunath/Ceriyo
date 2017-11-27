using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Services used for managing game instances.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Executes once to initialize the service.
        /// </summary>
        /// <param name="graphics">The graphics device manager provided by MonoGame.</param>
        void Initialize(IGraphicsDeviceManager graphics);

        /// <summary>
        /// The update method run during the game loop.
        /// This should be used for game logic.
        /// </summary>
        /// <param name="gameTime">The MonoGame gameTime object.</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// The draw method run during the game loop.
        /// This should be used for rendering.
        /// </summary>
        /// <param name="gameTime">The MonoGame gameTime object.</param>
        void Draw(GameTime gameTime);

        /// <summary>
        /// Fires when the game has exited. 
        /// Clean up of unmanaged objects should happen here.
        /// </summary>
        void Exit();
    }
}
