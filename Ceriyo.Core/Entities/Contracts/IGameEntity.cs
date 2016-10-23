using Artemis;

namespace Ceriyo.Core.Entities.Contracts
{
    public interface IGameEntity
    {
        void BuildEntity(Entity entity);
    }

    public interface IGameEntity<in T>
        where T : class
    {
        void BuildEntity(Entity entity, T args);
    }

}
