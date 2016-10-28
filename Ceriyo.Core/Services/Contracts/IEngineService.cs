namespace Ceriyo.Core.Services.Contracts
{
    public interface IEngineService
    {
        int GroundTileHeight { get; }
        int TileWidth { get; }
        int TileHeight { get; }
        int MaxAreaWidth { get; }
        int MaxAreaHeight { get; }

    }
}
