using Artemis;

namespace Ceriyo.Core.Contracts
{
    public interface IScriptService
    {
        void QueueScript(string fileName, Entity entity, string methodName = "Main");
        void ExecuteQueuedScripts();
    }
}
