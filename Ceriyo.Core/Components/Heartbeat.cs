using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Heartbeat: IComponent
    {
        public float Interval { get; set; }
        public float CurrentTimer { get; set; }

        public Heartbeat()
        {
            Interval = 6.0f;
            CurrentTimer = 0.0f;
        }
    }
}
