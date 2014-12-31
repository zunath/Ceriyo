using System;

namespace Ceriyo.Data.EventArguments
{
    public class DirectConnectEventArgs : EventArgs
    {
        public string Password { get; set; }
        public string IPAddress { get; set; }

        public DirectConnectEventArgs(string ipAddress, string password)
        {
            Password = password;
            IPAddress = ipAddress;
        }
    }
}
