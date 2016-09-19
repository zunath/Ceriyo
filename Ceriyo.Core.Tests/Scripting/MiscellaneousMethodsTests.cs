using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting
{
    [TestFixture]
    public class MiscellaneousMethodsTests
    {
        [Test]
        public void Print_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(
                delegate {
                    MiscellaneousMethods.Print("Test");
                });
        }

        [Test]
        public void GetScriptName_ShouldReturnValue()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Script scriptComponent = new Script()
            {
                Name = "TestScriptName"
            };
            entity.AddComponent(scriptComponent);
            string result = MiscellaneousMethods.GetScriptName(entity);
            Assert.AreEqual(result, "TestScriptName");
        }

        [Test]
        public void GetScriptName_NoComponent_ShouldReturnEmptyString()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            string result = MiscellaneousMethods.GetScriptName(entity);
            Assert.AreEqual(result, string.Empty);
        }
    }
}
