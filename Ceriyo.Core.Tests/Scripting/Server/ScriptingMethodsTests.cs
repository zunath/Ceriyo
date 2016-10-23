using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting.Server
{
    [TestFixture]
    public class ScriptingMethodsTests
    {
        private ScriptingMethods _scriptingMethods;

        [SetUp]
        public void SetUp()
        {
            _scriptingMethods = new ScriptingMethods();
        }

        [Test]
        public void GetScriptName_ShouldReturnValue()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            ScriptGroup scriptGroup = new ScriptGroup();
            scriptGroup.Add(ScriptEvent.OnAreaEnter, "TestScriptName");

            entity.AddComponent(scriptGroup);
            string result = _scriptingMethods.GetScriptName(entity, ScriptEvent.OnAreaEnter);
            Assert.AreEqual(result, "TestScriptName");
        }

        [Test]
        public void GetScriptName_NoComponent_ShouldReturnEmptyString()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            string result = _scriptingMethods.GetScriptName(entity, ScriptEvent.OnAreaEnter);
            Assert.AreEqual(result, string.Empty);
        }
    }
}
