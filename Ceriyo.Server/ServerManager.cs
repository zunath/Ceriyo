using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Server
{
    public class ServerManager
    {
        public ServerStatus Status { get; set; }

        public ServerManager()
        {
            this.Status = new ServerStatus();
        }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public void Shutdown()
        {
        }

    }
}
