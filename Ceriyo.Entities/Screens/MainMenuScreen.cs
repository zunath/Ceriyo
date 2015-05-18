﻿using System.Net;
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.GUI;
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
            SubscribePacketActions();
            GUI.OnDirectConnect += GUI_OnDirectConnect;
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            UnsubscribePacketActions();
            GUI.OnDirectConnect -= GUI_OnDirectConnect;
            GUI.Destroy();
        }

        private void SubscribePacketActions()
        {
            NetworkManager.SubscribePacketAction(typeof(UserConnectedPacket), ReceiveUserConnectedPacket);
            NetworkManager.SubscribePacketAction(typeof(UserInfoPacket), ReceiveUserInfoPacket);
        }

        private void UnsubscribePacketActions()
        {
            NetworkManager.UnsubscribePacketAction(typeof(UserConnectedPacket), ReceiveUserConnectedPacket);
            NetworkManager.UnsubscribePacketAction(typeof(UserInfoPacket), ReceiveUserInfoPacket);
        }

        private void GUI_OnDirectConnect(object sender, DirectConnectEventArgs e)
        {
            IPAddress address;

            if (IPAddress.TryParse(e.IPAddress, out address))
            {
                NetworkManager.ConnectToServer(e.IPAddress, e.Password);
            }
        }

        private void ReceiveUserConnectedPacket(PacketBase packetBase)
        {
            MoveToScreen(typeof(CharacterSelectionScreen));
        }

        private void ReceiveUserInfoPacket(PacketBase packetBase)
        {
            UserInfoPacket response = new UserInfoPacket
            {
                IsRequest = false,
                Username = "zunath" // TODO: Store username after it's been authorized by the master server
            };

            response.Send(NetDeliveryMethod.ReliableUnordered);
        }
    }
}
