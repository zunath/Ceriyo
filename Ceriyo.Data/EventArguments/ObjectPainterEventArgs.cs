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
        public int TileCellX { get; set; }
        public int TileCellY { get; set; }
        public IGameObject GameObject { get; set; }


        public ObjectPainterEventArgs(int tileCellX, int tileCellY)
        {
            this.TileCellX = tileCellX;
            this.TileCellY = tileCellY;
            this.GameObject = null;
        }

        public ObjectPainterEventArgs(IGameObject gameObject)
        {
            this.TileCellX = 0;
            this.TileCellY = 0;
            this.GameObject = gameObject;
        }
    }
}
