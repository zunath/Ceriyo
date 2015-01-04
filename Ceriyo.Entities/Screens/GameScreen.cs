
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using System;
using System.Collections.Generic;
using Ceriyo.Network;
using Ceriyo.Network.Packets;
using Lidgren.Network;
namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private PlayerEntity PC { get; set; }
        private GameMenuLogic GUI { get; set; }


        public GameScreen()
            : base("GameScreen")
        {
            GUI = new GameMenuLogic();
        }

        protected override void CustomInitialize()
        {
            GameGlobal.OnPacketReceived += PacketReceived;
            RequestInitializationPacket();
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            if (PC != null)
            {
                PC.Activity();
            }
        }

        protected override void CustomDestroy()
        {
            GameGlobal.OnPacketReceived -= PacketReceived;
            GUI.Destroy();
            if (PC != null)
            {
                PC.Destroy();
            }
        }

        #region Packet Processing

        private void PacketReceived(object sender, PacketEventArgs e)
        {
            Type type = e.Packet.GetType();

            if (type == typeof(GameScreenPacket))
            {
                ProcessGameScreenPacket(e.Packet as GameScreenPacket);
            }
        }

        private void ProcessGameScreenPacket(GameScreenPacket packet)
        {
            PC = new PlayerEntity();
            PC.InitializeEntity(false);


        }

        private void RequestInitializationPacket()
        {
            GameScreenPacket packet = new GameScreenPacket
            {
                IsRequest = true
            };

            GameGlobal.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
