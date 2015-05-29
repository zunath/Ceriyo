using System;
using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Library.Network.Packets;
using Lidgren.Network;

namespace Ceriyo.Library.Network
{
    public static class NetworkManager
    {
        private static NetworkAgent Agent { get; set; }
        private static List<Tuple<Type, PacketAction>> _packetActions;
        public static event EventHandler<ConnectionStatusEventArgs> OnConnected;
        public static event EventHandler<ConnectionStatusEventArgs> OnDisconnected;
        public static event EventHandler<ConnectionStatusEventArgs> OnDisconnecting;

        public static void Initialize(NetworkAgentRole networkRole, int port)
        {
            Agent = new NetworkAgent(networkRole, null, port);
            _packetActions = new List<Tuple<Type, PacketAction>>();
            Agent.OnConnected += RaiseOnConnectedAgentEvent;
            Agent.OnDisconnected += RaiseOnDisconnectedAgentEvent;
            Agent.OnDisconnecting += RaiseOnDisconnectingAgentEvent;
        }

        public static void Update()
        {
            ProcessPackets();
        }

        private static void ProcessPackets()
        {
            List<PacketBase> packets = Agent.CheckForPackets();

            foreach (PacketBase packet in packets)
            {
                var actions = _packetActions.Where(x => x.Item1 == packet.GetType()).ToList();

                foreach (var action in actions)
                {
                    action.Item2(packet);
                }
            }
        }

        public static void SubscribePacketAction(Type packetType, PacketAction action)
        {
            _packetActions.Add(new Tuple<Type, PacketAction>(packetType, action));
        }

        public static void UnsubscribePacketAction(Type packetType, PacketAction action)
        {
            _packetActions.RemoveAll(x => x.Item1 == packetType && x.Item2 == action);
        }


        public static NetworkAgentRole GetNetworkRole()
        {
            return Agent.Role;
        }

        public static NetConnection GetConnectionToServer()
        {
            if (Agent.Role == NetworkAgentRole.Server)
            {
                throw new Exception("Only clients can get the server connection.");
            }

            return Agent.Connections[0];
        }

        public static void ConnectToServer(string ipAddress, string password)
        {
            Agent.Connect(ipAddress, password);
        }

        public static void DisconnectFromServer()
        {
            Agent.Disconnect();
        }

        public static void SendPacket(PacketBase packet, NetConnection connection, NetDeliveryMethod deliveryMethod)
        {
            Agent.SendPacket(packet, connection, deliveryMethod);
        }

        public static void Shutdown()
        {
            Agent.Shutdown();
        }

        private static void RaiseOnConnectedAgentEvent(object sender, ConnectionStatusEventArgs e)
        {
            if (OnConnected != null)
            {
                OnConnected(sender, e);
            }
        }

        private static void RaiseOnDisconnectedAgentEvent(object sender, ConnectionStatusEventArgs e)
        {
            if (OnDisconnected != null)
            {
                OnDisconnected(sender, e);
            }
        }

        private static void RaiseOnDisconnectingAgentEvent(object sender, ConnectionStatusEventArgs e)
        {
            if (OnDisconnecting != null)
            {
                OnDisconnecting(sender, e);
            }
        }

    }
}
