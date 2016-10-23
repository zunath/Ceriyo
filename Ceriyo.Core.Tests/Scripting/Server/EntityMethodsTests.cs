using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting.Server
{
    [TestFixture]
    public class EntityMethodsTests
    {
        private EntityMethods _entityMethods;

        [SetUp]
        public void SetUp()
        {
            _entityMethods = new EntityMethods();
        }

        [Test]
        public void GetName_ShouldReturnValue()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Nameable nameableComponent = new Nameable
            {
                Value = "MyName"
            };
            entity.AddComponent(nameableComponent);
            string name = _entityMethods.GetName(entity);

            Assert.AreEqual(name, "MyName");
        }

        [Test]
        public void GetName_NoComponent_ShouldReturnEmptyString()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            string name = _entityMethods.GetName(entity);

            Assert.AreEqual(name, string.Empty);
        }
    }
}
