using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.Server
{
    public class ServerStartupArgs
    {
        public string ServerPassword { get; set; }
        public int Port { get; set; }

        public ServerStartupArgs()
        {
            this.ServerPassword = string.Empty;
            this.Port = 0;
        }
    }
}
