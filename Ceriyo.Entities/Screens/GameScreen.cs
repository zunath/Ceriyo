using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network.Packets;
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
            SubscribePacketActions();

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
            UnsubscribePacketActions();
            GUI.Destroy();
            if (PC != null)
            {
                PC.Destroy();
            }
        }

        #region Packet Processing

        private void SubscribePacketActions()
        {
            CeriyoServices.SubscribePacketAction(typeof(GameScreenPacket), ProcessGameScreenPacket);
        }

        private void UnsubscribePacketActions()
        {
            CeriyoServices.UnsubscribePacketAction(typeof(GameScreenPacket), ProcessGameScreenPacket);
        }

        private void ProcessGameScreenPacket(PacketBase packetBase)
        {
            PC = new PlayerEntity();
            PC.InitializeEntity(false);
        }

        #endregion
    }
}
