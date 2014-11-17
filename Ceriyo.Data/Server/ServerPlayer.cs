using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.Server
{
    public class ServerPlayer
    {
        public string Username { get; set; }
        public Player PC { get; set; }

        public ServerPlayer()
        {
            Username = string.Empty;
            PC = new Player();
        }
    }
}
