using System;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Creates screens for use with the Entity-Component-System
    /// </summary>
    public interface IScreenFactory
    {
        /// <summary>
        /// Creates a screen.
        /// </summary>
        /// <typeparam name="T">The type of screen to create.</typeparam>
        /// <returns>The created screen.</returns>
        IScreen Create<T>() where T : IScreen;

        /// <summary>
        /// Creates a screen.
        /// </summary>
        /// <param name="type">The type of screen to create.</param>
        /// <returns>The created screen.</returns>
        IScreen Create(Type type);
    }
}
