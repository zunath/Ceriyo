using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.Server
{
    public class ServerStartupArgs
    {
        public int Port { get; set; }

        public ServerStartupArgs()
        {
            this.Port = 0;
        }
    }
}
