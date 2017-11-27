using Artemis;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    /// <summary>
    /// Script methods used for manipulating physics.
    /// </summary>
    public interface IPhysicsMethods
    {
        /// <summary>
        /// Returns the X velocity of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the X velocity from.</param>
        /// <returns>The X velocity of the entity.</returns>
        float GetVelocityX(Entity entity);

        /// <summary>
        /// Returns the Y velocity of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the Y velocity from.</param>
        /// <returns>The Y velocity of the entity.</returns>
        float GetVelocityY(Entity entity);

        /// <summary>
        /// Sets the X velocity of an entity to a new value.
        /// </summary>
        /// <param name="entity">The entity to adjust.</param>
        /// <param name="x">The new X velocity.</param>
        void SetVelocityX(Entity entity, float x);

        /// <summary>
        /// Sets the Y velocity of an entity to a new value.
        /// </summary>
        /// <param name="entity">The entity to adjust.</param>
        /// <param name="y">The new Y velocity.</param>
        void SetVelocityY(Entity entity, float y);

        /// <summary>
        /// Returns the X position of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the X position from.</param>
        /// <returns>The X position of the entity.</returns>
        float GetPositionX(Entity entity);

        /// <summary>
        /// Returns the Y position of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the Y position from.</param>
        /// <returns>The Y position of the entity.</returns>
        float GetPositionY(Entity entity);

        /// <summary>
        /// Returns the directional facing of an entity.
        /// </summary>
        /// <param name="entity">The entity to retrieve the facing from.</param>
        /// <returns>The direction the entity is facing.</returns>
        Direction GetFacing(Entity entity);

        /// <summary>
        /// Sets the facing of an entity to a new value.
        /// </summary>
        /// <param name="entity">The entity to adjust.</param>
        /// <param name="facing">The new directional facing.</param>
        void SetFacing(Entity entity, Direction facing);
    }
}
