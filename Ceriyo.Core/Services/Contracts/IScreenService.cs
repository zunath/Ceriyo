using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Service which manages screens.
    /// </summary>
    public interface IScreenService
    {
        /// <summary>
        /// Switches the current screen to a new one.
        /// </summary>
        /// <typeparam name="T">The type of screen to create and switch to.</typeparam>
        void ChangeScreen<T>()
            where T : IScreen;

        /// <summary>
        /// Updates the state of the screen service. 
        /// Call this in the game loop's Update() method.
        /// </summary>
        void Update();

        /// <summary>
        /// Renders the screen service.
        /// Call this in the game loop's Draw() method.
        /// </summary>
        void Draw();
    }
}
