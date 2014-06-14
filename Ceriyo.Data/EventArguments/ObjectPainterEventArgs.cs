using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
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
            this.TileCellXStart = tileCellXStart;
            this.TileCellYStart = tileCellYStart;
            this.TileCellXEnd = tileCellXEnd;
            this.TileCellYEnd = tileCellYEnd;
            this.GameObject = null;
        }

        public ObjectPainterEventArgs(IGameObject gameObject)
        {
            this.TileCellXStart = 0;
            this.TileCellYStart = 0;
            this.GameObject = gameObject;
        }
    }
}
