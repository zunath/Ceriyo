using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Scripting;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting
{
    [TestFixture]
    public class PhysicsMethodsTests
    {
        private Entity BuildValidEntity()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Velocity velocityComponent = new Velocity
            {
                Y = 15.0f,
                X = 10.0f
            };
            entity.AddComponent(velocityComponent);
            Position positionComponent = new Position
            {
                X = 40,
                Y = 100,
                Facing = Direction.North
            };
            entity.AddComponent(positionComponent);

            return entity;
        }

        public Entity BuildInvalidEntity()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            return entity;
        }

        [Test]
        public void GetVelocityX_ShouldReturn10()
        {
            Entity entity = BuildValidEntity();
            float x = PhysicsMethods.GetVelocityX(entity);
            Assert.AreEqual(x, 10.0f);
        }

        [Test]
        public void GetVelocityX_NoComponent_ShouldReturnZero()
        {
            Entity entity = BuildInvalidEntity();
            float x = PhysicsMethods.GetVelocityX(entity);
            Assert.AreEqual(x, 0.0f);
        }

        [Test]
        public void GetVelocityY_ShouldReturn15()
        {
            Entity entity = BuildValidEntity();
            float y = PhysicsMethods.GetVelocityY(entity);
            Assert.AreEqual(y, 15.0f);
        }

        [Test]
        public void GetVelocityY_NoComponent_ShouldReturnZero()
        {
            Entity entity = BuildInvalidEntity();
            float y = PhysicsMethods.GetVelocityY(entity);
            Assert.AreEqual(y, 0.0f);
        }

        [Test]
        public void SetVelocityX_ShouldBe44()
        {
            Entity entity = BuildValidEntity();
            PhysicsMethods.SetVelocityX(entity, 44.0f);
            Assert.AreEqual(entity.GetComponent<Velocity>().X, 44.0f);
        }

        [Test]
        public void SetVelocityX_NoComponent_ShouldNotThrowException()
        {
            Entity entity = BuildInvalidEntity();
            Assert.DoesNotThrow(delegate
            {
                PhysicsMethods.SetVelocityX(entity, 44.0f);
            });
        }

        [Test]
        public void SetVelocityY_ShouldBe33()
        {
            Entity entity = BuildValidEntity();
            PhysicsMethods.SetVelocityY(entity, 33.0f);
            Assert.AreEqual(entity.GetComponent<Velocity>().Y, 33.0f);
        }

        [Test]
        public void SetVelocityY_NoComponent_ShouldNotThrowException()
        {
            Entity entity = BuildInvalidEntity();
            Assert.DoesNotThrow(delegate
            {
                PhysicsMethods.SetVelocityY(entity, 33.0f);
            });
        }

        [Test]
        public void GetPositionX_ShouldEqual40()
        {
            Entity entity = BuildValidEntity();
            float x = PhysicsMethods.GetPositionX(entity);
            Assert.AreEqual(x, 40.0f);
        }

        [Test]
        public void GetPositionX_NoComponent_ShouldEqualZero()
        {
            Entity entity = BuildInvalidEntity();
            float x = PhysicsMethods.GetPositionX(entity);
            Assert.AreEqual(x, 0.0f);
        }

        [Test]
        public void GetPositionY_ShouldEqual100()
        {
            Entity entity = BuildValidEntity();
            float y = PhysicsMethods.GetPositionY(entity);
            Assert.AreEqual(y, 100.0f);
        }

        [Test]
        public void GetPositionY_NoComponent_ShouldEqualZero()
        {
            Entity entity = BuildInvalidEntity();
            float y = PhysicsMethods.GetPositionY(entity);
            Assert.AreEqual(y, 0.0f);
        }

        [Test]
        public void GetFacing_ShouldEqualNorth()
        {
            Entity entity = BuildValidEntity();
            Direction facing = PhysicsMethods.GetFacing(entity);
            Assert.AreEqual(facing, Direction.North);
        }

        [Test]
        public void GetFacing_NoComponent_ShouldEqualUnknown()
        {
            Entity entity = BuildInvalidEntity();
            Direction facing = PhysicsMethods.GetFacing(entity);
            Assert.AreEqual(facing, Direction.Unknown);
        }
        [Test]
        public void SetFacing_ShouldEqualSouth()
        {
            Entity entity = BuildValidEntity();
            PhysicsMethods.SetFacing(entity, Direction.South);

            Assert.AreEqual(entity.GetComponent<Position>().Facing, Direction.South);
        }
        [Test]
        public void SetFacing_NoComponent_ShouldNotThrowException()
        {
            Entity entity = BuildInvalidEntity();
            Assert.DoesNotThrow(() => PhysicsMethods.SetFacing(entity, Direction.South));
        }
        
    }
}
