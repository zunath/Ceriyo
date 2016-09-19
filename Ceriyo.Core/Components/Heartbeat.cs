using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Heartbeat: IComponent
    {
        public float Interval { get; set; }
        public float CurrentTimer { get; set; }
    }
}
