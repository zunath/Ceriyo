using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Actions
{
    public class PlayerDisconnectedAction: IServerAction
    {
        public string Username { get; set; }

        public void Process()
        {
            
        }
    }
}
