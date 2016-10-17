using Artemis;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Scripting;
using Ceriyo.Testing.Shared;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
                ScriptEngine.Lua,
                entity);

            Assert.AreEqual(queue.FilePath, "testFilePath");
            Assert.AreEqual(queue.MethodName, "testMethodName");
            Assert.AreEqual(queue.EngineType, ScriptEngine.Lua);
            Assert.AreSame(queue.TargetObject, entity);
        }
    }
}
