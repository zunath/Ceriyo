namespace Ceriyo.Core.EventArgs
{
    public class NetworkConnectionEventArgs: System.EventArgs
    {
        public string Username { get; set; }

        public NetworkConnectionEventArgs(string username)
        {
            Username = username;
        }
    }
}
