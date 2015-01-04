using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using FlatRedBall;
using FlatRedBall.Graphics;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Ceriyo.Entities.Screens
{
    public class EnteringGameScreen : BaseScreen
    {
        private Text LoadingText { get; set;}
        private NetworkTransferData _transferData;

        public EnteringGameScreen()
            : base("EnteringGameScreen")
        {
            LoadingText = TextManager.AddText("Loading...");
            LoadingText.VerticalAlignment = VerticalAlignment.Center;
            LoadingText.HorizontalAlignment = HorizontalAlignment.Center;

            _transferData = new NetworkTransferData();
            SpriteManager.Camera.BackgroundColor = Color.Black;
        }

        protected override void CustomInitialize()
        {
            CeriyoServices.OnPacketReceived += ReceivePacket;
            CeriyoServices.Agent.OnConnected += Agent_OnConnected;
            CeriyoServices.Agent.OnDisconnected += Agent_OnDisconnected;
            CeriyoServices.OnPacketReceived += ReceivePacket;

            EnteringGameScreenPacket packet = new EnteringGameScreenPacket
            {
                IsRequest = true
            };

            packet.Send(NetDeliveryMethod.ReliableUnordered);
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            
        }

        protected override void CustomDestroy()
        {
            CeriyoServices.Agent.OnConnected -= Agent_OnConnected;
            CeriyoServices.Agent.OnDisconnected -= Agent_OnDisconnected;
            CeriyoServices.OnPacketReceived -= ReceivePacket;
            TextManager.RemoveText(LoadingText);
        }

        private void ReceivePacket(object sender, PacketEventArgs e)
        {
            _transferData = e.Packet.ClientReceive(_transferData);
        }

        private void Agent_OnConnected(object sender, Data.EventArguments.ConnectionStatusEventArgs e)
        {

        }


        private void Agent_OnDisconnected(object sender, Data.EventArguments.ConnectionStatusEventArgs e)
        {
            
        }

    }
}
