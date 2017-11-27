using Artemis;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    /// <summary>
    /// Scripting methods used for entities.
    /// </summary>
    public interface IEntityMethods
    {
        /// <summary>
        /// Gets the name of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the name of.</param>
        /// <returns>The name of the entity.</returns>
        string GetName(Entity entity);

        /// <summary>
        /// Gets the tag of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the tag of.</param>
        /// <returns>The tag of the entity.</returns>
        string GetTag(Entity entity);

        /// <summary>
        /// Gets the resref of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the resref of.</param>
        /// <returns>The resref of the entity.</returns>
        string GetResref(Entity entity);

        /// <summary>
        /// Sets the name of an entity to a new name.
        /// </summary>
        /// <param name="entity">The entity to set the name of.</param>
        /// <param name="value">The new name to use.</param>
        void SetName(Entity entity, string value);

        /// <summary>
        /// Sets the tag of an entity to a new tag.
        /// </summary>
        /// <param name="entity">The entity to set the tag of.</param>
        /// <param name="value">The new tag to use.</param>
        void SetTag(Entity entity, string value);
    }
}
