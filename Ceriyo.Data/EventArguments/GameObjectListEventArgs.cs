using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class GameObjectListEventArgs : EventArgs
    {
        public IList<IGameObject> GameObjects { get; set; }

        public GameObjectListEventArgs(IList<IGameObject> gameObjects)
        {
            this.GameObjects = gameObjects;
        }
    }
}
