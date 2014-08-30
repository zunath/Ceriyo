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
using Ceriyo.Library.Global;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private MainMenuLogic GUI { get; set; }

        public GameScreen()
            : base("GameScreen")
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
            GUI.Destroy();
            GameGlobal.Agent.Disconnect();
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
                NetConnection conn = GameGlobal.Agent.Connect(e.IPAddress, e.Password);

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
                    Username = "zunath" // TODO: Get username
                };

                GameGlobal.Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
            }
        }

    }
}
