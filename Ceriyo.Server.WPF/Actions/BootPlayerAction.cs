using Ceriyo.Core.Contracts;
using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Actions
{
    // This object is created on the server GUI thread,
    // but processed on the server logic thread.
    // For that reason, the service locator pattern is used
    // in this instance. (The GUI thread intentionally has no 
    // registration for the network service.)
    public class BootPlayerAction: IServerAction
    {
        public string Username { get; set; }

        public void Process()
        {
            IServerNetworkService service = ServerIOCConfig.Resolve<IServerNetworkService>();

            service.BootUsername(Username);
        }
    }
}
