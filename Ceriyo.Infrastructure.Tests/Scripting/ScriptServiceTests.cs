using System.IO;
using Ceriyo.Core.Scripting.Client;
using Ceriyo.Core.Scripting.Common;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Logging;
using Ceriyo.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Ceriyo.Infrastructure.Tests.Scripting
{
    [TestFixture]
    public class ScriptServiceTests
    {
        private ScriptService _service;

        [SetUp]
        public void Setup()
        {
            var engineService = new EngineService();
            var mockUIService = new Mock<IUIService>();


            _service = new ScriptService(false, 
                new Logger(),
                new LoggingMethods(), 
                new ControlMethods(),
                new StyleMethods(), 
                new EntityMethods(engineService), 
                new LocalDataMethods(), 
                new PhysicsMethods(), 
                new ScriptingMethods(),
                new SceneMethods(mockUIService.Object));
        }

        [Test]
        public void QueueScriptLua_ShouldNotThrowExceptions()
        {
            string tempFilePath = "./Scripts/Tests/TestScript.lua";
            Directory.CreateDirectory("./Scripts/Tests");
            FileStream stream = File.Create(tempFilePath);
            stream.Close();
            Assert.DoesNotThrow(delegate
            {
                _service.QueueScript("Tests/TestScript.lua", null);
            });
            
            File.Delete(tempFilePath);
            Directory.Delete("./Scripts/Tests/");
        }

        [Test]
        public void QueueScriptLua_NoFile_ShouldThrowException()
        {
            Assert.Throws(typeof(FileNotFoundException), delegate
            {
                _service.QueueScript("nonexistentfile.lua", null);
            });
        }

        [Test]
        public void ExecuteScriptLua_ShouldNotThrowException()
        {
            string tempFilePath = "./Scripts/Tests/TestScript.lua";
            string scriptBody = "function Main() end";
            Directory.CreateDirectory("./Scripts/Tests");
            File.WriteAllText(tempFilePath, scriptBody);

            Assert.DoesNotThrow(delegate
            {
                _service.QueueScript("Tests/TestScript.lua", null);
                _service.ExecuteQueuedScripts();
            });


            File.Delete(tempFilePath);
            Directory.Delete("./Scripts/Tests/");
        }

        [Test]
        public void QueueScriptJavaScript_ShouldNotThrowExceptions()
        {
            string tempFilePath = "./Scripts/Tests/TestScript.js";
            Directory.CreateDirectory("./Scripts/Tests");
            FileStream stream = File.Create(tempFilePath);
            stream.Close();
            Assert.DoesNotThrow(delegate
            {
                _service.QueueScript("Tests/TestScript.js", null);
            });

            File.Delete(tempFilePath);
            Directory.Delete("./Scripts/Tests/");
        }

        [Test]
        public void QueueScriptJavaScript_NoFile_ShouldThrowException()
        {
            Assert.Throws(typeof(FileNotFoundException), delegate
            {
                _service.QueueScript("nonexistentfile.js", null);
            });
        }

        [Test]
        public void ExecuteScriptJavaScript_ShouldNotThrowException()
        {
            string tempFilePath = "./Scripts/Tests/TestScript.js";
            string scriptBody = "function Main() {}";
            Directory.CreateDirectory("./Scripts/Tests");
            File.WriteAllText(tempFilePath, scriptBody);

            Assert.DoesNotThrow(delegate
            {
                _service.QueueScript("Tests/TestScript.js", null);
                _service.ExecuteQueuedScripts();
            });
            
            File.Delete(tempFilePath);
            Directory.Delete("./Scripts/Tests/");
        }

    }
}
