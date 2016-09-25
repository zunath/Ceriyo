using Artemis;

namespace Ceriyo.Core.Entities.Contracts
{
    public interface IGameEntity
    {
        void BuildEntity(Entity entity, params object[] args);
    }
}
