using System;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Handles engine-level logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="message">The arbitrary message to log.</param>
        void Error(object message);

        /// <summary>
        /// Logs an error with exception details.
        /// </summary>
        /// <param name="message">The arbitrary message to log.</param>
        /// <param name="ex">The exception to log.</param>
        void Error(object message, Exception ex);

        /// <summary>
        /// Logs an informational detail.
        /// </summary>
        /// <param name="message">The arbitrary message to log.</param>
        void Info(object message);
    }
}
