using System;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class ObjectPainterEventArgs : EventArgs
    {
        public int TileCellXStart { get; set; }
        public int TileCellYStart { get; set; }
        public int TileCellXEnd { get; set; }
        public int TileCellYEnd { get; set; }
        public IGameObject GameObject { get; set; }


        public ObjectPainterEventArgs(int tileCellXStart, int tileCellYStart, int tileCellXEnd, int tileCellYEnd)
        {
            TileCellXStart = tileCellXStart;
            TileCellYStart = tileCellYStart;
            TileCellXEnd = tileCellXEnd;
            TileCellYEnd = tileCellYEnd;
            GameObject = null;
        }

        public ObjectPainterEventArgs(IGameObject gameObject)
        {
            TileCellXStart = 0;
            TileCellYStart = 0;
            GameObject = gameObject;
        }
    }
}
