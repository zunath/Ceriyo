﻿using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    public class EngineService: IEngineService
    {
        public string ApplicationIdentifier => "8C09885A-38D5-4351-8387-C983BC7AB36A";
        public int TileWidth => 64;
        public int TileHeight => 32;
        public int MaxAreaWidth => 32;
        public int MaxAreaHeight => 32;

        public int MaxNameLength => 256;
        public int MaxTagLength => 64;
        public int MaxResrefLength => 32;
    }
}
