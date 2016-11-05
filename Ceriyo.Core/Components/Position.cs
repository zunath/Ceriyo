using Artemis.Interface;
using Ceriyo.Core.Constants;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Components
{
    public class Position: IComponent
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Direction Facing { get; set; }

        private Vector2 _vector;

        public Position()
        {
            _vector = new Vector2(X, Y);
        }

        public Vector2 ToVector2()
        {
            _vector.X = X;
            _vector.Y = Y;
            return _vector;
        }

    }
}
