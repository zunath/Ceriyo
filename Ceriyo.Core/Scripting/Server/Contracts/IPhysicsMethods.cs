using Artemis;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    public interface IPhysicsMethods
    {
        float GetVelocityX(Entity entity);
        float GetVelocityY(Entity entity);
        void SetVelocityX(Entity entity, float x);
        void SetVelocityY(Entity entity, float y);
        float GetPositionX(Entity entity);
        float GetPositionY(Entity entity);
        Direction GetFacing(Entity entity);
        void SetFacing(Entity entity, Direction facing);
    }
}
