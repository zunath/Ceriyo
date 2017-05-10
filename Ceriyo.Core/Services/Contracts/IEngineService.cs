﻿namespace Ceriyo.Core.Services.Contracts
{
    public interface IEngineService
    {
        string ApplicationIdentifier { get; }
        int TileWidth { get; }
        int TileHeight { get; }
        int MaxAreaWidth { get; }
        int MaxAreaHeight { get; }
        int MaxNameLength { get; }
        int MaxTagLength { get; }
        int MaxResrefLength { get; }

    }
}
