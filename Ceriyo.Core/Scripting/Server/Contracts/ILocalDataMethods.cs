using Artemis;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    public interface ILocalDataMethods
    {
        void SetLocalValue(Entity entity, string key, string value);
        void SetLocalValue(Entity entity, string key, double value);
        string GetLocalString(Entity entity, string key);
        double GetLocalNumber(Entity entity, string key);
        void DeleteLocalString(Entity entity, string key);
        void DeleteLocalNumber(Entity entity, string key);
    }
}
