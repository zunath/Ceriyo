namespace Ceriyo.Core.Contracts
{
    public interface IClientNetworkService
    {
        void ConnectToServer(string ipAddress, int port, string username, string password);
    }
}
