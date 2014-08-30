using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.EventArguments
{
    public class DirectConnectEventArgs : EventArgs
    {
        public string Password { get; set; }
        public string IPAddress { get; set; }

        public DirectConnectEventArgs(string ipAddress, string password)
        {
            this.Password = password;
            this.IPAddress = ipAddress;
        }
    }
}
