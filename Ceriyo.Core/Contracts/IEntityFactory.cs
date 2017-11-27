using Artemis;
using Ceriyo.Core.Entities.Contracts;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Creates entities for use in the Entity-Component-System
    /// </summary>
    public interface IEntityFactory
    {
        /// <summary>
        /// Creates a game entity.
        /// </summary>
        /// <typeparam name="T">The type of IGameEntity to create.</typeparam>
        /// <returns>The created ECS entity.</returns>
        Entity Create<T>() 
            where T : IGameEntity;

        /// <summary>
        /// Creates a game entity using default data.
        /// </summary>
        /// <typeparam name="TEntity">The type of IGameEntity to create.</typeparam>
        /// <typeparam name="TArgument">The type of data to pass in for construction.</typeparam>
        /// <param name="args">The data object to use for creation.</param>
        /// <returns>The created ECS entity.</returns>
        Entity Create<TEntity, TArgument>(TArgument args)
            where TEntity: IGameEntity<TArgument>
            where TArgument: class;
    }
}
