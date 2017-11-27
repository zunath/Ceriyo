using Artemis.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks rendering information for an entity.
    /// </summary>
    public class Renderable: IComponent
    {
        /// <summary>
        /// The texture of the entity.
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// The drawable source
        /// </summary>
        public Rectangle Source { get; set; }
        /// <summary>
        /// The color to override the sprite with.
        /// If null, the default value will be used. (Normally Color.White)
        /// </summary>
        public Color? ColorOverride { get; set; }
        /// <summary>
        /// The origin of the renderable.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Constructs a new renderable component.
        /// </summary>
        public Renderable()
        {
            Origin = Vector2.Zero;
        }
    }
}
