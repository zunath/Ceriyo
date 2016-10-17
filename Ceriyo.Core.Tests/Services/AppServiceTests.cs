using System.IO;
using Ceriyo.Core.Services;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Services
{
    public class AppServiceTests
    {
        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists("./Modules"))
            {
                Directory.Delete("./Modules", true);
            }
            if (Directory.Exists("./ResourcePacks"))
            {
                Directory.Delete("./ResourcePacks", true);
            }
            if (Directory.Exists("./ServerVault"))
            {
                Directory.Delete("./ServerVault", true);
            }
            if (Directory.Exists("./Settings"))
            {
                Directory.Delete("./Settings", true);
            }
        }

        [Test]
        public void AppService_CreateAppDirectoryStructure_ShouldCreateAllFolders()
        {
            AppService service = new AppService();
            service.CreateAppDirectoryStructure();

            Assert.IsTrue(Directory.Exists("./Modules"));
            Assert.IsTrue(Directory.Exists("./ResourcePacks"));
            Assert.IsTrue(Directory.Exists("./ServerVault"));
            Assert.IsTrue(Directory.Exists("./Settings"));



        }
    }
}
