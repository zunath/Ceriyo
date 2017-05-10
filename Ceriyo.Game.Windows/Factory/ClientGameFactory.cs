using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Game.Windows.Factory
{
    public static class ClientGameFactory
    {
        private static IGameService _gameService;

        public static IGameService GetClientGameService()
        {
            return _gameService ?? (_gameService = ClientGameIOCConfig.Resolve<IGameService>());
        }
    }
}
