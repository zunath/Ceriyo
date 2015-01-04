using System;
using System.Net;
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;

namespace Ceriyo.Entities.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        private MainMenuLogic GUI { get; set; }
        private NetworkTransferData _transferData;

        public MainMenuScreen()
            : base("MainMenu")
        {
            GUI = new MainMenuLogic();
        }

        protected override void CustomInitialize()
        {
            GUI.OnDirectConnect += GUI_OnDirectConnect;
            CeriyoServices.OnPacketReceived += ReceivePacket;
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            GUI.OnDirectConnect -= GUI_OnDirectConnect;
            CeriyoServices.OnPacketReceived -= ReceivePacket;
            GUI.Destroy();
        }

        private void ReceivePacket(object sender, PacketEventArgs e)
        {
            _transferData = e.Packet.ClientReceive(_transferData);

            PacketBase packet = e.Packet;
            Type type = packet.GetType();

            if (type == typeof(UserConnectedPacket))
            {
                ReceiveUserConnectedPacket(packet as UserConnectedPacket);
            }
        }

        private void GUI_OnDirectConnect(object sender, DirectConnectEventArgs e)
        {
            IPAddress address;

            if (IPAddress.TryParse(e.IPAddress, out address))
            {
                CeriyoServices.Agent.Connect(e.IPAddress, e.Password);
            }
        }

        private void ReceiveUserConnectedPacket(UserConnectedPacket packet)
        {
            MoveToScreen(typeof(CharacterSelectionScreen));
        }
    }
}
