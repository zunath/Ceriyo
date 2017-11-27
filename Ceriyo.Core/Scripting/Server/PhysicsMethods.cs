using System;
using Artemis;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Scripting.Server.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    /// <inheritdoc />
    [ScriptNamespace("Physics")]
    public class PhysicsMethods: IPhysicsMethods
    {

        /// <inheritdoc />
        public float GetVelocityX(Entity entity)
        {
            try
            {
                return entity.GetComponent<Velocity>().X;
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }

        /// <inheritdoc />
        public float GetVelocityY(Entity entity)
        {
            try
            {
                return entity.GetComponent<Velocity>().Y;
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }

        /// <inheritdoc />
        public void SetVelocityX(Entity entity, float x)
        {
            if (entity.HasComponent<Velocity>())
            {
                entity.GetComponent<Velocity>().X = x;
            }
        }

        /// <inheritdoc />
        public void SetVelocityY(Entity entity, float y)
        {
            if (entity.HasComponent<Velocity>())
            {
                entity.GetComponent<Velocity>().Y = y;
            }
        }

        /// <inheritdoc />
        public float GetPositionX(Entity entity)
        {
            try
            {
                return entity.GetComponent<Position>().X;
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }

        /// <inheritdoc />
        public float GetPositionY(Entity entity)
        {
            try
            {
                return entity.GetComponent<Position>().Y;
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }

        /// <inheritdoc />
        public Direction GetFacing(Entity entity)
        {
            try
            {
                return entity.GetComponent<Position>().Facing;
            }
            catch (Exception)
            {
                return Direction.Unknown;
            }
        }

        /// <inheritdoc />
        public void SetFacing(Entity entity, Direction facing)
        {
            if (entity.HasComponent<Position>())
            {
                entity.GetComponent<Position>().Facing = facing;
            }
        }
    }
}
