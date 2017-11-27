using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    /// <inheritdoc />
    public class EngineService: IEngineService
    {
        /// <inheritdoc />
        public string ApplicationIdentifier => "8C09885A-38D5-4351-8387-C983BC7AB36A";

        /// <inheritdoc />
        public int TileWidth => 64;

        /// <inheritdoc />
        public int TileHeight => 32;

        /// <inheritdoc />
        public int MaxAreaWidth => 32;

        /// <inheritdoc />
        public int MaxAreaHeight => 32;

        /// <inheritdoc />
        public int MaxNameLength => 256;

        /// <inheritdoc />
        public int MaxTagLength => 64;

        /// <inheritdoc />
        public int MaxResrefLength => 32;
    }
}
