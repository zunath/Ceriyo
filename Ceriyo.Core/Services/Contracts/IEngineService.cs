namespace Ceriyo.Core.Services.Contracts
{
    public interface IEngineService
    {
        int TileWidth { get; }
        int TileHeight { get; }
        int MaxAreaWidth { get; }
        int MaxAreaHeight { get; }

    }
}
