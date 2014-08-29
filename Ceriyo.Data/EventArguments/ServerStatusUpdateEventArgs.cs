using Ceriyo.Data.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.EventArguments
{
    public class ServerStatusUpdateEventArgs: EventArgs
    {
        public BindingList<string> ConnectedUsernames { get; set; }
        public BindingList<string> MessageLog { get; set; }
        public bool GameJustStarted { get; set; }
        public bool GameJustShutDown { get; set; }

        public ServerStatusUpdateEventArgs()
        {
            this.ConnectedUsernames = new BindingList<string>();
            this.MessageLog = new BindingList<string>();
            this.GameJustStarted = false;
            this.GameJustShutDown = false;
        }
    }
}
