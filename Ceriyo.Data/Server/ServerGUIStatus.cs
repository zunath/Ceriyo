using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.Server
{
    public class ServerGUIStatus
    {
        public ServerSettings Settings { get; set; }
        public bool IsServerRunning { get; set; }

        public ServerGUIStatus()
        {
            Settings = new ServerSettings();
            IsServerRunning = false;
        }
    }
}
