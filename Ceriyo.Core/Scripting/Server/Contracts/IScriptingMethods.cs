using Artemis;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    public interface IScriptingMethods
    {
        string GetScriptName(Entity entity, ScriptEvent @event);
    }
}
