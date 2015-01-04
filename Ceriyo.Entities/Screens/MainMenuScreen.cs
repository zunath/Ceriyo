﻿using System;
using System.Net;
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
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
        }

        protected override void CustomInitialize()
        {
            HookEvents();
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            UnhookEvents();
            GUI.Destroy();
        }


        private void HookEvents()
        {
            GUI.OnDirectConnect += GUI_OnDirectConnect;
            CeriyoServices.Agent.OnConnected += Agent_OnConnected;
            CeriyoServices.Agent.OnDisconnected += Agent_OnDisconnected;
            CeriyoServices.OnPacketReceived += PacketReceived;
        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {
            
        }

        private void Agent_OnConnected(object sender, ConnectionStatusEventArgs e)
        {
            
        }

        private void PacketReceived(object sender, PacketEventArgs e)
        {
            PacketBase packet = e.Packet;
            Type type = packet.GetType();

            if (type == typeof(UserInfoPacket))
            {
                ReceiveUserInfoPacket(packet as UserInfoPacket);
            }
            else if (type == typeof(UserConnectedPacket))
            {
                ReceiveUserConnectedPacket(packet as UserConnectedPacket);
            }
        }

        private void UnhookEvents()
        {
            GUI.OnDirectConnect -= GUI_OnDirectConnect;
            CeriyoServices.Agent.OnConnected -= Agent_OnConnected;
            CeriyoServices.Agent.OnDisconnected -= Agent_OnDisconnected;
            CeriyoServices.OnPacketReceived -= PacketReceived;
        }

        private void GUI_OnDirectConnect(object sender, DirectConnectEventArgs e)
        {
            IPAddress address;

            if (IPAddress.TryParse(e.IPAddress, out address))
            {
                CeriyoServices.Agent.Connect(e.IPAddress, e.Password);
                
            }
        }

        private void ReceiveUserInfoPacket(UserInfoPacket packet)
        {
            if (packet.IsRequest)
            {
                UserInfoPacket response = new UserInfoPacket
                {
                    IsRequest = false,
                    Username = "zunath" // TODO: Store username after it's been authorized by the master server
                };

                response.Send(NetDeliveryMethod.ReliableUnordered, packet.SenderConnection);
            }
        }

        private void ReceiveUserConnectedPacket(UserConnectedPacket packet)
        {
            MoveToScreen(typeof(CharacterSelectionScreen));
        }
    }
}
