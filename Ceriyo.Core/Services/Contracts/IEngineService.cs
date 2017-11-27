namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Constant values used by the engine.
    /// </summary>
    public interface IEngineService
    {
        /// <summary>
        /// A unique GUID used for identifying the application.
        /// </summary>
        string ApplicationIdentifier { get; }

        /// <summary>
        /// The width of tiles.
        /// </summary>
        int TileWidth { get; }

        /// <summary>
        /// The height of tiles.
        /// </summary>
        int TileHeight { get; }

        /// <summary>
        /// Maximum width of areas, in tiles.
        /// </summary>
        int MaxAreaWidth { get; }

        /// <summary>
        /// Maximum height of areas, in tiles.
        /// </summary>
        int MaxAreaHeight { get; }

        /// <summary>
        /// Maximum length of names.
        /// </summary>
        int MaxNameLength { get; }

        /// <summary>
        /// Maximum length of tags.
        /// </summary>
        int MaxTagLength { get; }

        /// <summary>
        /// Maximum length of resrefs.
        /// </summary>
        int MaxResrefLength { get; }

    }
}
