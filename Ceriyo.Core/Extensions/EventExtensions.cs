using System;

namespace Ceriyo.Core.Extensions
{
    public static class EventExtensions
    {
        public static void RaiseEvent(this EventHandler @event, object sender)
        {
            @event?.Invoke(sender, new System.EventArgs());
        }

        public static void RaiseEvent<T>(this EventHandler<T> @event, object sender, T eventArgs)
            where T: System.EventArgs
        {
            @event?.Invoke(sender, eventArgs);
        }
    }
}
