namespace Ceriyo.Toolset.WPF.EventArgs
{
    public class TileSelectedEventArgs
    {
        public int CellX { get; set; }
        public int CellY { get; set; }

        public TileSelectedEventArgs()
        {
            CellX = 0;
            CellY = 0;
        }

        public TileSelectedEventArgs(int cellX, int cellY)
        {
            CellX = cellX;
            CellY = cellY;
        }
    }
}
