using System;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Data.EventArguments
{
    public class TilePaintEventArgs : EventArgs
    {
        public int Layer { get; set; }
        public int StartCellX { get; set; }
        public int StartCellY { get; set; }
        public int EndCellX { get; set; }
        public int EndCellY { get; set; }

        public int StartTextureCellX { get; set; }
        public int StartTextureCellY { get; set; }
        public int EndTextureCellX { get; set; }
        public int EndTextureCellY { get; set; }

        public Texture2D Texture { get; set; }

        public TilePaintEventArgs(int startCellX,
                                 int startCellY,
                                 int endCellX,
                                 int endCellY,
                                 int startTextureCellX,
                                 int startTextureCellY,
                                 int endTextureCellX,
                                 int endTextureCellY,
                                 int layer,
                                 Texture2D texture)
        {
            Layer = layer;
            StartCellX = startCellX;
            StartCellY = startCellY;
            EndCellX = endCellX;
            EndCellY = endCellY;
            Texture = texture;

            StartTextureCellX = startTextureCellX;
            StartTextureCellY = startTextureCellY;
            EndTextureCellX = endTextureCellX;
            EndTextureCellY = endTextureCellY;
        }
    }
}
