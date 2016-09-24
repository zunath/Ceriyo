using System.IO;
using Ceriyo.Infrastructure.Services;
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
            _service = new ScriptService();
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
