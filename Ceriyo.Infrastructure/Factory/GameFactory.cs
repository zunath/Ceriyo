﻿using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.IOC;

namespace Ceriyo.Infrastructure.Factory
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
