using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Server.WPF.Factory
{
    public static class ServerGameFactory
    {
        public static IGameService GetServerGameService()
        {
            return ServerIOCConfig.Resolve<IGameService>();
        }
    }
}
