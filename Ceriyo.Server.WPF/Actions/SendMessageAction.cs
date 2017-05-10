using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Actions
{
    public class SendMessageAction: IServerAction
    {
        public string Message { get; set; }
        public void Process()
        {
            
        }
    }
}
