using System;

namespace Ceriyo.Core.Extensions
{
    /// <summary>
    /// Extensions for EventHandler objects.
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Invokes events if there are any.
        /// </summary>
        /// <param name="event">The event handler</param>
        /// <param name="sender">The sender of the event.</param>
        public static void RaiseEvent(this EventHandler @event, object sender)
        {
            @event?.Invoke(sender, new EventArgs());
        }

        /// <summary>
        /// Invokes events if there are any and passes event args.
        /// </summary>
        /// <typeparam name="T">The type of event args to send when the event is raised.</typeparam>
        /// <param name="event">The event handler</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="eventArgs">The event args object to pass with the event raise.</param>
        public static void RaiseEvent<T>(this EventHandler<T> @event, object sender, T eventArgs)
            where T: EventArgs
        {
            @event?.Invoke(sender, eventArgs);
        }
    }
}
