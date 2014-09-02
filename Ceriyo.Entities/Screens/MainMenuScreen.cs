using System;
using System.Collections.Generic;
using System.Net;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Lidgren.Network;

namespace Ceriyo.Entities.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        private MainMenuLogic GUI { get; set; }

        public MainMenuScreen()
            : base("MainMenu")
        {
            GUI = new MainMenuLogic();
            GameGlobal.Agent = new NetworkAgent(NetworkAgentRoleEnum.Client, null, 5121);
        }

        protected override void CustomInitialize()
        {
            HookEvents();
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            ProcessPackets();
        }

        protected override void CustomDestroy()
        {
            UnhookEvents();
            GUI.Destroy();
        }


        private void HookEvents()
        {
            GUI.OnDirectConnect += GUI_OnDirectConnect;
            GameGlobal.Agent.OnConnected += Agent_OnConnected;
            GameGlobal.Agent.OnDisconnected += Agent_OnDisconnected;
        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {
            
        }

        private void Agent_OnConnected(object sender, ConnectionStatusEventArgs e)
        {
            
        }

        private void UnhookEvents()
        {
            GUI.OnDirectConnect -= GUI_OnDirectConnect;
            GameGlobal.Agent.OnConnected -= Agent_OnConnected;
            GameGlobal.Agent.OnDisconnected -= Agent_OnDisconnected;
        }

        private void GUI_OnDirectConnect(object sender, DirectConnectEventArgs e)
        {
            IPAddress address;

            if (IPAddress.TryParse(e.IPAddress, out address))
            {
                GameGlobal.Agent.Connect(e.IPAddress, e.Password);
                
            }
        }

        private void ProcessPackets()
        {
            List<PacketBase> packets = GameGlobal.Agent.CheckForPackets();

            foreach (PacketBase packet in packets)
            {
                Type type = packet.GetType();

                if (type == typeof(UserInfoPacket))
                {
                    ReceiveUserInfoPacket(packet as UserInfoPacket);
                }
            }
        }

        private void ReceiveUserInfoPacket(UserInfoPacket packet)
        {
            if (packet.IsRequest)
            {
                UserInfoPacket response = new UserInfoPacket
                {
                    IsRequest = false,
                    Username = GameGlobal.Username
                };

                GameGlobal.Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
            }
        }
    }
}
