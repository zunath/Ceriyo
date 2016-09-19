using Artemis.Interface;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Components
{
    public class Position: IComponent
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Direction Facing { get; set; }
    }
}
