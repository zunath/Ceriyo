using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Actions
{
    public class PlayerConnectedAction: IServerAction
    {
        public string Username { get; set; }

        public void Process()
        {
            
        }
    }
}
