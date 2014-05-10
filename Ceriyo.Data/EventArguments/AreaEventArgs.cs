using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class GameObjectEventArgs : EventArgs
    {
        public IGameObject GameObject { get; set; }

        public GameObjectEventArgs(IGameObject gameObject)
        {
            this.GameObject = gameObject;
        }
    }
}
