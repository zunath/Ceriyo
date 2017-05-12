namespace Ceriyo.Server.WPF.Contracts
{
    public interface IServerActionService
    {
        void QueueAction(IServerAction action);
        void ProcessActions();
    }
}
