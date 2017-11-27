using System.Collections.Generic;
using Artemis;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Handles the engine scripting system.
    /// </summary>
    public interface IScriptService
    {
        /// <summary>
        /// Queues a script for processing.
        /// </summary>
        /// <param name="fileName">The script name to queue.</param>
        /// <param name="entity">The ECS entity to run the script on.</param>
        /// <param name="methodName">The method to fire. By default this is 'Main'.</param>
        void QueueScript(string fileName, Entity entity, string methodName = "Main");

        /// <summary>
        /// Executes all queued scripts.
        /// </summary>
        void ExecuteQueuedScripts();

        /// <summary>
        /// Returns the enumerations registered with the script service.
        /// </summary>
        /// <returns>A list of enumerations registered with the script service.</returns>
        IEnumerable<string> GetRegisteredEnumerations();

        /// <summary>
        /// Validates a script and returns the results.
        /// </summary>
        /// <param name="scriptText">The script text to validate.</param>
        /// <returns>Empty if the script is valid. Otherwise, returns the errors.</returns>
        string ValidateScript(string scriptText);
    }
}
