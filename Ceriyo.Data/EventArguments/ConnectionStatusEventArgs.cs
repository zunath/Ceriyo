using System;
using Lidgren.Network;

namespace Ceriyo.Data.EventArguments
{
    public class ConnectionStatusEventArgs : EventArgs
    {
        public NetConnection Connection { get; set; }
    }
}
