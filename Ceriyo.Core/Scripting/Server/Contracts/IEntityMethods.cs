using Artemis;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    public interface IEntityMethods
    {
        string GetName(Entity entity);
        string GetTag(Entity entity);
        string GetResref(Entity entity);

        void SetName(Entity entity, string value);
        void SetTag(Entity entity, string value);
    }
}
