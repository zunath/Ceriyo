using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Packets;
using Ceriyo.Core.Services.Contracts;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Services
{
    public class ClientNetworkService: IClientNetworkService
    {
        private NetClient _client;
        private readonly ILogger _logger;
        private readonly IEngineService _engineService;

        public ClientNetworkService(ILogger logger, IEngineService engineService)
        {
            _logger = logger;
            _engineService = engineService;
        }

        public void ConnectToServer(string ipAddress, int port, string username, string password)
        {
            ConnectionRequestPacket packet = new ConnectionRequestPacket
            {
                Username = username,
                Password = password
            };

            NetPeerConfiguration config = new NetPeerConfiguration(_engineService.ApplicationIdentifier);
            _client = new NetClient(config);
            NetOutgoingMessage message = _client.CreateMessage();
            MemoryStream stream = new MemoryStream();
            Serializer.SerializeWithLengthPrefix(stream, packet, PrefixStyle.Base128);
            message.Write(stream.ToArray());

            _client.Start();
            _client.Connect(ipAddress, port, message);
        }
    }
}
