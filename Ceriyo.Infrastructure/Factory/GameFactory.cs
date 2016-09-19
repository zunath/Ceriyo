using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.IOC;

namespace Ceriyo.Infrastructure.Factory
{
    public static class GameFactory
    {
        private static IGameService _gameService;

        public static IGameService GetGameService()
        {
            return _gameService ?? (_gameService = IOCConfig.Resolve<IGameService>());
        }
    }
}
