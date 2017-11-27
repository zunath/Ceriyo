namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Standard routines for game screens
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Initializes a screen with default values. Should only be run once.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Updates a screen as part of the game loop. Your game logic should go here.
        /// </summary>
        void Update();

        /// <summary>
        /// Draws a screen as part of the game loop. Your rendering code should go here.
        /// </summary>
        void Draw();

        /// <summary>
        /// Cleans up a screen as part of its closing process. Any objects the screen is managing should be disposed of here.
        /// </summary>
        void Close();
    }
}
