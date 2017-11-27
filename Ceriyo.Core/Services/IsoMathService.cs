using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Services
{
    /// <inheritdoc />
    public class IsoMathService: IIsoMathService
    {
        private readonly IEngineService _engineService;

        /// <inheritdoc />
        public IsoMathService(IEngineService engineService)
        {
            _engineService = engineService;
        }

        /// <inheritdoc />
        public Vector2 MapTileToScreenPosition(float mapX, float mapY)
        {
            float x = ((int)mapX - (int)mapY) * _engineService.TileWidth / 2.0f;
            float y = ((int)mapX + (int)mapY) * _engineService.TileHeight / 2.0f;

            return new Vector2(x, y);
        }

        /// <inheritdoc />
        public Vector2 MapTileToScreenPosition(int mapX, int mapY)
        {
            return MapTileToScreenPosition((float) mapX, (float) mapY);
        }

        /// <inheritdoc />
        public Vector2 MapTileToScreenPosition(Vector2 mapPosition)
        {
            return MapTileToScreenPosition(mapPosition.X, mapPosition.Y);
        }

        /// <inheritdoc />
        public Vector2 ScreenPositionToMapTile(float screenX, float screenY)
        {
            float halfWidth = _engineService.TileWidth / 2.0f;
            float halfHeight = _engineService.TileHeight / 2.0f;

            float x = (screenX / halfWidth + screenY / halfHeight) / 2;
            float y = (screenY / halfHeight - screenX / halfWidth) / 2;

            return new Vector2(x, y);
        }

        /// <inheritdoc />
        public Vector2 ScreenPositionToMapTile(int screenX, int screenY)
        {
            return ScreenPositionToMapTile((float)screenX, (float)screenY);
        }

        /// <inheritdoc />
        public Vector2 ScreenPositionToMapTile(Vector2 screenPosition)
        {
            return ScreenPositionToMapTile(screenPosition.X, screenPosition.Y);
        }
    }
}
