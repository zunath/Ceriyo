using FlatRedBall;
using FlatRedBall.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.DrawableBatches;
using Ceriyo.Library.SquidGUI;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall.IO;
using Squid;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Network;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Packets;
using Lidgren.Network;
using Ceriyo.Data.EventArguments;
using System.Net;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private NetworkAgent Agent { get; set; }
        private MainMenuLogic GUI { get; set; }

        public GameScreen()
            : base("GameScreen")
        {
            GUI = new MainMenuLogic();
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Client, 5121);
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
            GUI.Destroy();
            Agent.Shutdown();
        }


        private void HookEvents()
        {
            GUI.OnDirectConnect += GUI_OnDirectConnect;
        }

        private void GUI_OnDirectConnect(object sender, DirectConnectEventArgs e)
        {
            IPAddress address;

            if (IPAddress.TryParse(e.IPAddress, out address))
            {
                Agent.Connect(e.IPAddress);
            }
        }

        private void ProcessPackets()
        {
            List<PacketBase> packets = Agent.CheckForPackets();

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
                    Username = "zunath" // TODO: Get username
                };

                Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
            }
        }

    }
}
