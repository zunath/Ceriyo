using Artemis;
using Ceriyo.Core.Scripting;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting
{
    public class ScriptQueueObjectTests
    {
        [Test]
        public void ScriptQueueObject_Instantiate_ValuesShouldMatch()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            var entity = world.CreateEntity();

            ScriptQueueObject queue = new ScriptQueueObject(
                "testFilePath",
                "testMethodName",
                entity);

            Assert.AreEqual(queue.FilePath, "testFilePath");
            Assert.AreEqual(queue.MethodName, "testMethodName");
            Assert.AreSame(queue.TargetObject, entity);
        }
    }
}
