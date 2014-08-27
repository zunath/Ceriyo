using Ceriyo.Data.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.EventArguments
{
    public class ServerStatusUpdateEventArgs: EventArgs
    {
        public ServerGameStatus GameStatus { get; set; }

        public ServerStatusUpdateEventArgs()
        {
        }

        public ServerStatusUpdateEventArgs(ServerGameStatus status)
        {
            this.GameStatus = status;
        }
    }
}
