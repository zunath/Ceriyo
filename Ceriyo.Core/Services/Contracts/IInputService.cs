using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Service used for managing input state.
    /// </summary>
    public interface IInputService
    {
        /// <summary>
        /// Updates the input state. Should be called during the game loop's Update method.
        /// </summary>
        void Update();

        /// <summary>
        /// Gets whether or not the left mouse button is down.
        /// </summary>
        /// <returns>true if left mouse button is down, false otherwise.</returns>
        bool IsLeftMouseDown();

        /// <summary>
        /// Gets whether or not the left mouse button is up.
        /// </summary>
        /// <returns>true if left mouse button is up, false otherwise.</returns>
        bool IsLeftMouseUp();

        /// <summary>
        /// Gets whether or not the right mouse button is down.
        /// </summary>
        /// <returns>true if right mouse button is down, false otherwise.</returns>
        bool IsRightMouseDown();

        /// <summary>
        /// Gets whether or not the right mouse button is up.
        /// </summary>
        /// <returns>true if right mouse button is up, false otherwise.</returns>
        bool IsRightMouseUp();

        /// <summary>
        /// Gets whether or not the left mouse button was pressed.
        /// A "press" represents that the mouse was down in the previous frame but is up on the current frame
        /// </summary>
        /// <returns>true if the left mouse button was pressed, false otherwise.</returns>
        bool IsLeftMousePressed();

        /// <summary>
        /// Gets whether or not the right mouse button was pressed.
        /// A "press" represents that the mouse was down in the previous frame but is up on the current frame.
        /// </summary>
        /// <returns>true if the right mouse button was pressed, false otherwise.</returns>
        bool IsRightMousePressed();

        /// <summary>
        /// Gets the position of the mouse in screen coordinates.
        /// </summary>
        /// <returns>The screen coordinates of the mouse position.</returns>
        Vector2 GetMousePosition();
    }
}
