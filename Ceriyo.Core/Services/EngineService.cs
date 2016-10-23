﻿using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    public class EngineService: IEngineService
    {
        public int TileWidth => 64;
        public int TileHeight => 32;
        public int MaxAreaWidth => 32;
        public int MaxAreaHeight => 32;
    }
}
