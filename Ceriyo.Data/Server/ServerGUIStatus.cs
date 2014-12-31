using Ceriyo.Data.Settings;

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
