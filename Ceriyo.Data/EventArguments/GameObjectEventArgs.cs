using System;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class GameObjectEventArgs : EventArgs
    {
        public IGameObject GameObject { get; set; }

        public GameObjectEventArgs(IGameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}
