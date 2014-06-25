using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Data.Engine;
using System.IO;

namespace Ceriyo.Data.Tests
{
    [TestClass]
    public class EngineDataManagerTests
    {
        [TestMethod]
        public void EngineDataManager_DirectoriesMustExist()
        {
            EngineDataManager manager = new EngineDataManager();
            manager.InitializeEngine();

            bool exists = Directory.Exists(EnginePaths.ModulesDirectory);
            Assert.AreEqual(true, exists);

            exists = Directory.Exists(EnginePaths.ScriptsDirectory);
            Assert.AreEqual(true, exists);

            exists = Directory.Exists(EnginePaths.ResourcePacksDirectory);
            Assert.AreEqual(true, exists);
        }
    }
}
