using Ceriyo.Data.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.EventArguments
{
    public class ServerStatusUpdateEventArgs: EventArgs
    {
        public ServerGUIStatus GUIStatus { get; set; }
        public ServerGameStatus GameStatus { get; set; }

        public ServerStatusUpdateEventArgs(ServerGUIStatus guiStatus = null, ServerGameStatus gameStatus = null)
        {
            this.GUIStatus = guiStatus;
            this.GameStatus = gameStatus;
        }
    }
}
