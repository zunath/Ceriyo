
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using System;
using System.Collections.Generic;
using Lidgren.Network;
namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
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
        }

        protected override void CustomDestroy()
        {
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
        }

        private void RequestInitializationPacket()
        {
            GameScreenPacket packet = new GameScreenPacket
            {
                IsRequest = true
            };

            GameGlobal.Agent.SendPacket(packet, GameGlobal.Agent.Connections[0], NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
