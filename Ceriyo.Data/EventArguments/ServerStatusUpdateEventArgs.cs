using System;
using System.ComponentModel;

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
            ConnectedUsernames = new BindingList<string>();
            MessageLog = new BindingList<string>();
            GameJustStarted = false;
            GameJustShutDown = false;
        }
    }
}
