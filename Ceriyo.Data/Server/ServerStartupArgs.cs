namespace Ceriyo.Data.Server
{
    public class ServerStartupArgs
    {
        public string ServerPassword { get; set; }
        public int Port { get; set; }
        public string ModuleFileName { get; set; }

        public ServerStartupArgs()
        {
            ServerPassword = string.Empty;
            Port = 0;
            ModuleFileName = string.Empty;
        }
    }
}
