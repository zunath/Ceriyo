using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks the velocity of an entity.
    /// </summary>
    public class Velocity: IComponent
    {
        /// <summary>
        /// The X velocity
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// The Y velocity
        /// </summary>
        public float Y { get; set; }
    }
}
