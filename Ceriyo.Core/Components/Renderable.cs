using Artemis.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Components
{
    public class Renderable: IComponent
    {
        public Texture2D Texture { get; set; }
        public Rectangle Source { get; set; }
        public Color? ColorOverride { get; set; }
        public Vector2 Origin { get; set; }

        public Renderable()
        {
            Origin = Vector2.Zero;
        }
    }
}
