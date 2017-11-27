using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Handles common isometric math functions.
    /// </summary>
    public interface IIsoMathService
    {
        /// <summary>
        /// Converts a map tile to a screen position.
        /// </summary>
        /// <param name="mapX">The X position of the tile on the map.</param>
        /// <param name="mapY">The Y position of the tile on the map.</param>
        /// <returns>The screen position of the tile.</returns>
        Vector2 MapTileToScreenPosition(float mapX, float mapY);


        /// <summary>
        /// Converts a map tile to a screen position.
        /// </summary>
        /// <param name="mapX">The X position of the tile on the map.</param>
        /// <param name="mapY">The Y position of the tile on the map.</param>
        /// <returns>The screen position of the tile.</returns>
        Vector2 MapTileToScreenPosition(int mapX, int mapY);

        /// <summary>
        /// Converts a map tile to a screen position.
        /// </summary>
        /// <param name="mapPosition">The X and Y </param>
        /// <returns></returns>
        Vector2 MapTileToScreenPosition(Vector2 mapPosition);

        /// <summary>
        /// Converts a screen position to a map tile.
        /// </summary>
        /// <param name="screenX">The X position on the screen.</param>
        /// <param name="screenY">The Y position on the screen.</param>
        /// <returns></returns>
        Vector2 ScreenPositionToMapTile(float screenX, float screenY);


        /// <summary>
        /// Converts a screen position to a map tile.
        /// </summary>
        /// <param name="screenX">The X position on the screen.</param>
        /// <param name="screenY">The Y position on the screen.</param>
        /// <returns></returns>
        Vector2 ScreenPositionToMapTile(int screenX, int screenY);


        /// <summary>
        /// Converts a screen position to a map tile.
        /// </summary>
        /// <param name="screenPosition">The X and Y positions on the screen.</param>
        /// <returns></returns>
        Vector2 ScreenPositionToMapTile(Vector2 screenPosition);
    }
}
