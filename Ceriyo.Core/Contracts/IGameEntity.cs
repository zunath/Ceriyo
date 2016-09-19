using Artemis;

namespace Ceriyo.Core.Contracts
{
    public interface IGameEntity
    {
        void BuildEntity(Entity entity, params object[] args);
    }
}
