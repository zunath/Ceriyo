using Artemis.Interface;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Components
{
    public class CollisionBox: IComponent
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsVisible { get; set; }
        public Color Color { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public CollisionBox()
        {
            Color = Color.White;
            IsVisible = true;
        }
    }
}
