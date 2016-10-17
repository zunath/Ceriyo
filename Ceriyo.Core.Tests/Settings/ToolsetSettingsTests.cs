using System.Reflection;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Settings;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Settings
{
    public class ToolsetSettingsTests
    {
        [Test]
        public void ToolsetSettings_FilePathAttribute_ShouldMatch()
        {
            ToolsetSettings settings = new ToolsetSettings();
            FilePathAttribute attr = (FilePathAttribute)settings.GetType().GetCustomAttribute(typeof(FilePathAttribute));

            Assert.AreEqual(attr.FilePath, "./Settings/Toolset.settings");
        }

    }
}
