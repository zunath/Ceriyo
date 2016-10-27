using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Map: IComponent
    {
        public int Width => Tiles.GetUpperBound(0) + 1;
        public int Height => Tiles.GetUpperBound(1) + 1;
        public Tile[,] Tiles { get; set; }


        public void Resize(int newWidth, int newHeight)
        {
            Tile[,] newTiles = new Tile[newWidth, newHeight];

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    if (Width > x && Height > y)
                    {
                        newTiles[x, y] = Tiles[x, y];
                    }
                }
            }

            Tiles = newTiles;
        }

    }
}
