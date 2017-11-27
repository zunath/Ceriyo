using Artemis;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    /// <summary>
    /// Scripting methods used for adjusting scripts.
    /// </summary>
    public interface IScriptingMethods
    {
        /// <summary>
        /// Returns the name of a script on a particular script event.
        /// </summary>
        /// <param name="entity">The entity to retrieve from.</param>
        /// <param name="event">The script event to retrieve.</param>
        /// <returns>Empty string if not found, otherwise returns the name of the script.</returns>
        string GetScriptName(Entity entity, ScriptEvent @event);
    }
}
