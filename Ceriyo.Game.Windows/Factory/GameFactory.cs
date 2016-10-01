using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Game.Windows.Factory
{
    public static class GameFactory
    {
        private static IGameService _gameService;

        public static IGameService GetGameService()
        {
            return _gameService ?? (_gameService = GameIOCConfig.Resolve<IGameService>());
        }
    }
}
