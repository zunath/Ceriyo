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
        public void QueueScript_ShouldNotThrowExceptions()
        {
            string tempFilePath = "./Scripts/TestFile.lua";
            Directory.CreateDirectory("./Scripts");
            FileStream stream = File.Create(tempFilePath);
            stream.Close();
            Assert.DoesNotThrow(delegate
            {
                _service.QueueScript("TestFile.lua", null);
            });
            File.Delete(tempFilePath);
            Directory.Delete("./Scripts");
        }

        [Test]
        public void QueueScript_NoFile_ShouldThrowException()
        {
            Assert.Throws(typeof(FileNotFoundException), delegate
            {
                _service.QueueScript("nonexistentfile.lua", null);
            });
        }

        [Test]
        public void ExecuteScript_ShouldNotThrowException()
        {
            string tempFilePath = "./Scripts/TestScript.lua";
            string scriptBody = "function Main() end";
            Directory.CreateDirectory("./Scripts");
            File.WriteAllText(tempFilePath, scriptBody);

            Assert.DoesNotThrow(delegate
            {
                _service.QueueScript("TestScript.lua", null);
                _service.ExecuteQueuedScripts();
            });


            File.Delete(tempFilePath);
            Directory.Delete("./Scripts");
        }

    }
}
