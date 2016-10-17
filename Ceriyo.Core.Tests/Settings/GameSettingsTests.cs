using System.Reflection;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Settings;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Settings
{
    public class GameSettingsTests
    {
        [Test]
        public void GameSettings_FilePathAttribute_ShouldMatch()
        {
            GameSettings settings = new GameSettings();
            FilePathAttribute attr = (FilePathAttribute) settings.GetType().GetCustomAttribute(typeof(FilePathAttribute));

            Assert.AreEqual(attr.FilePath, "./Settings/Game.settings");
        }
    }
}
