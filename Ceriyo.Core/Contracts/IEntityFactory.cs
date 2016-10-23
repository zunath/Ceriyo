using Artemis;
using Ceriyo.Core.Entities.Contracts;

namespace Ceriyo.Core.Contracts
{
    public interface IEntityFactory
    {
        Entity Create<T>() 
            where T : IGameEntity;

        Entity Create<TEntity, TArgument>(TArgument args)
            where TEntity: IGameEntity<TArgument>
            where TArgument: class;
    }
}
