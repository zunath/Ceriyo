using System;
using Artemis;

namespace Ceriyo.Core.Extensions
{
    /// <summary>
    /// Extensions for the Artemis EntityWorld object.
    /// </summary>
    public static class EntityWorldExtensions
    {
        /// <summary>
        /// Gets the world delta in seconds.
        /// </summary>
        /// <param name="world">The game world.</param>
        /// <returns>The world delta in seconds.</returns>
        public static float DeltaSeconds(this EntityWorld world)
        {
            return TimeSpan.FromTicks(world.Delta).Milliseconds * 0.001f;
        }
    }
}
