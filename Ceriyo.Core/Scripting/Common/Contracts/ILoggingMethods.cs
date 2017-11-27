namespace Ceriyo.Core.Scripting.Common.Contracts
{
    /// <summary>
    /// Scripting methods used for logging.
    /// </summary>
    public interface ILoggingMethods
    {
        /// <summary>
        /// Prints a message to the log.
        /// </summary>
        /// <param name="message"></param>
        void Print(string message);
    }
}
