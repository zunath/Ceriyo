using Artemis.Interface;
using Ceriyo.Core.Constants;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks the position of an entity.
    /// </summary>
    public class Position: IComponent
    {
        /// <summary>
        /// The X position 
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// The Y position
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// The direction facing of the entity.
        /// </summary>
        public Direction Facing { get; set; }

        private Vector2 _vector;

        /// <summary>
        /// Constructs a new position.
        /// </summary>
        public Position()
        {
            _vector = new Vector2(X, Y);
        }

        /// <summary>
        /// Converts the position to a Vector2
        /// </summary>
        /// <returns>The position in Vector2 format.</returns>
        public Vector2 ToVector2()
        {
            _vector.X = X;
            _vector.Y = Y;
            return _vector;
        }

    }
}
