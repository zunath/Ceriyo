using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Heartbeat: IComponent
    {
        /// <summary>
        /// The amount of time (in seconds) the heartbeat will fire.
        /// </summary>
        public float Interval { get; set; }
        /// <summary>
        /// The heartbeat's current timer. When this meets or exceeds Interval, the heartbeat should be fired.
        /// </summary>
        public float CurrentTimer { get; set; }

        public Heartbeat()
        {
            Interval = 6.0f;
            CurrentTimer = 0.0f;
        }
    }
}
