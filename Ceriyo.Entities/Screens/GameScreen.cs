using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using System;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using Lidgren.Network;
namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private PlayerEntity PC { get; set; }
        private GameMenuLogic GUI { get; set; }
        private NetworkTransferData _transferData;

        public GameScreen()
            : base("GameScreen")
        {
            GUI = new GameMenuLogic();
            _transferData = new NetworkTransferData();
        }

        protected override void CustomInitialize()
        {
            CeriyoServices.OnPacketReceived += ReceivePacket;

            GameScreenPacket packet = new GameScreenPacket
            {
                IsRequest = true
            };

            packet.Send(NetDeliveryMethod.ReliableUnordered);
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
            CeriyoServices.OnPacketReceived -= ReceivePacket;
            GUI.Destroy();
            if (PC != null)
            {
                PC.Destroy();
            }
        }

        #region Packet Processing

        private void ReceivePacket(object sender, PacketEventArgs e)
        {
            _transferData = e.Packet.ClientReceive(_transferData);

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

        #endregion
    }
}
