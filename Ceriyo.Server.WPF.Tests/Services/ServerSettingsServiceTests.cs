using System.Linq;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Settings;
using Ceriyo.Server.WPF.Services;
using NUnit.Framework;

namespace Ceriyo.Server.WPF.Tests.Services
{
    public class ServerSettingsServiceTests
    {
        [Test]
        public void ServerSettingsService_CopySettings_ShouldMatch()
        {
            ServerSettingsService service = new ServerSettingsService();
            ServerSettings settings = new ServerSettings
            {
                ServerName = "Server2",
                Port = 42,
                PVPType = PVPType.Party,
                MaxPlayers = 54,
                AllowCharacterDeletion = true,
                AllowFileDownloading = true,
                Announcement = "test announcement",
                Description = "test description",
                GMPassword = "gmpassword",
                GameCategory = GameCategory.PWAction,
                PlayerPassword = "playerpassword"
            };

            settings.Blacklist.Add("testbl1");
            settings.Blacklist.Add("testbl2");
            settings.Blacklist.Add("testbl3");

            service.CopySettings(settings);

            Assert.AreEqual(settings.ServerName, service.ServerName);
            Assert.AreEqual(settings.Port, service.Port);
            Assert.AreEqual(settings.PVPType, service.PVPType);
            Assert.AreEqual(settings.AllowCharacterDeletion, service.AllowCharacterDeletion);
            Assert.AreEqual(settings.AllowFileDownloading, service.AllowFileDownloading);
            Assert.AreEqual(settings.Announcement, service.Announcement);
            Assert.AreEqual(settings.Description, service.Description);
            Assert.AreEqual(settings.GMPassword, service.GMPassword);
            Assert.AreEqual(settings.GameCategory, service.GameCategory);
            Assert.AreEqual(settings.PlayerPassword, service.PlayerPassword);

            Assert.IsTrue(service.BlackList.Contains("testbl1"));
            Assert.IsTrue(service.BlackList.Contains("testbl2"));
            Assert.IsTrue(service.BlackList.Contains("testbl3"));

            Assert.AreEqual(service.BlackList.Count, 3);
        }
    }
}
