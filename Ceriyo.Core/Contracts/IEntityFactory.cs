using Artemis;
using Ceriyo.Core.Entities.Contracts;

namespace Ceriyo.Core.Contracts
{
    public interface IEntityFactory
    {
        Entity Create<T>(params object[] args) where T : IGameEntity;
    }
}
