using Artemis.Interface;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks collision box information used with sprites and other collidable objects.
    /// </summary>
    public class CollisionBox: IComponent
    {
        /// <summary>
        /// Width of the box
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of the box
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Whether or not it will be rendered. (default: true)
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Color of the box (default: Color.White)
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// X position offset
        /// </summary>
        public int OffsetX { get; set; }
        /// <summary>
        /// Y position offset
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// Constructs a collision box.
        /// </summary>
        public CollisionBox()
        {
            Color = Color.White;
            IsVisible = true;
        }
    }
}
