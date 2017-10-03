using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Contracts
{
    public interface IIsoMathService
    {
        Vector2 MapTileToScreenPosition(float mapX, float mapY);
        Vector2 MapTileToScreenPosition(int mapX, int mapY);
        Vector2 MapTileToScreenPosition(Vector2 mapPosition);
        Vector2 ScreenPositionToMapTile(float screenX, float screenY);
        Vector2 ScreenPositionToMapTile(int screenX, int screenY);
        Vector2 ScreenPositionToMapTile(Vector2 screenPosition);
    }
}
