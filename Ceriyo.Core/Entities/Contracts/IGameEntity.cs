using Artemis;

namespace Ceriyo.Core.Entities.Contracts
{
    /// <summary>
    /// Identifies a game entity for use in the Entity-Component-System.
    /// </summary>
    public interface IGameEntity
    {
        /// <summary>
        /// Builds an entity.
        /// </summary>
        /// <param name="entity">The ECS entity to build into. 
        /// This entity will be attached with all of the necessary components.</param>
        void BuildEntity(Entity entity);
    }

    /// <summary>
    /// Identifies a game entity for use in the Entity-Component-System
    /// </summary>
    /// <typeparam name="T">The type of data used when creating the entity.</typeparam>
    public interface IGameEntity<in T>
        where T : class
    {
        /// <summary>
        /// Builds an entity.
        /// </summary>
        /// <param name="entity">The ECS entity to build into.
        /// This entity will be attached with all of the necessary components.</param>
        /// <param name="args">Data to be passed in while building the entity.</param>
        void BuildEntity(Entity entity, T args);
    }

}
