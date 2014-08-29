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

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private NetworkAgent Agent { get; set; }
        private MainMenuLogic _mainMenuGUI;

        public GameScreen()
            : base("GameScreen")
        {
            _mainMenuGUI = new MainMenuLogic();
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Client, 5121);
        }

        protected override void CustomInitialize()
        {
            HookEvents();

            // DEBUGGING

            Agent.Connect("127.0.0.1");

            // END DEBUGGING

        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            ProcessPackets();
        }

        protected override void CustomDestroy()
        {
            _mainMenuGUI.Destroy();
            Agent.Shutdown();
        }


        private void HookEvents()
        {

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
