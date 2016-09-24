using System.IO;
using Ceriyo.Infrastructure.Scripting;
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

            var fullPath = Path.GetFullPath(tempFilePath);

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

    }
}
