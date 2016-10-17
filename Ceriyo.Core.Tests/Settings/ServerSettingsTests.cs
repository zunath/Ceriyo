using System.Reflection;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Settings;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Settings
{
    public class ServerSettingsTests
    {
        [Test]
        public void ServerSettings_FilePathAttribute_ShouldMatch()
        {
            ServerSettings settings = new ServerSettings();
            FilePathAttribute attr = (FilePathAttribute)settings.GetType().GetCustomAttribute(typeof(FilePathAttribute));

            Assert.AreEqual(attr.FilePath, "./Settings/Server.settings");
        }
    }
}
