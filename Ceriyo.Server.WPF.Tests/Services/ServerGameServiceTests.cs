using System;
using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Server.WPF.Services;
using Ceriyo.Testing.Shared;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace Ceriyo.Server.WPF.Tests.Services
{
    public class ServerGameServiceTests
    {
        [Test]
        public void ServerGameService_Draw_ShouldThrowNotSupportedException()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();

            ServerGameService service = new ServerGameService(
                world, 
                new Mock<IServerSettingsService>().Object,
                new Mock<IScriptService>().Object,
                new Mock<IDataService>().Object,
                new Mock<IAppService>().Object,
                new Mock<IServerNetworkService>().Object);

            Assert.Throws<NotSupportedException>(() =>
            {
                service.Draw(new GameTime());
            });
        }

    }
}
