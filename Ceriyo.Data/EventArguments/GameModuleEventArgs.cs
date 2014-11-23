using System;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class GameModuleEventArgs : EventArgs
    {
        public GameModule Module { get; private set; }

        public GameModuleEventArgs(GameModule module)
        {
            Module = module;
        }
    }
}
