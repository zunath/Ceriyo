using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class GameModuleEventArgs : EventArgs
    {
        public GameModule Module { get; set; }

        public GameModuleEventArgs(GameModule module)
        {
            this.Module = module;
        }
    }
}
