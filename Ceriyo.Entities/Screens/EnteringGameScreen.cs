using Ceriyo.Library.Global;
using Ceriyo.Network;
using Ceriyo.Network.Packets;
using FlatRedBall;
using FlatRedBall.Graphics;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Ceriyo.Entities.Screens
{
    public class EnteringGameScreen : BaseScreen
    {
        private Text LoadingText { get; set;} 

        public EnteringGameScreen()
            : base("EnteringGameScreen")
        {
            LoadingText = TextManager.AddText("Loading...");
            LoadingText.VerticalAlignment = VerticalAlignment.Center;
            LoadingText.HorizontalAlignment = HorizontalAlignment.Center;


            SpriteManager.Camera.BackgroundColor = Color.Black;
        }

        protected override void CustomInitialize()
        {
            HookEvents();

            EnteringGameScreenPacket packet = new EnteringGameScreenPacket
            {
                IsRequest = true
            };

            GameGlobal.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            
        }

        protected override void CustomDestroy()
        {
            UnhookEvents();
            TextManager.RemoveText(LoadingText);
        }

        private void HookEvents()
        {
            GameGlobal.Agent.OnConnected += Agent_OnConnected;
            GameGlobal.Agent.OnDisconnected += Agent_OnDisconnected;
            GameGlobal.OnPacketReceived += OnPacketReceived;
        }

        private void UnhookEvents()
        {
            GameGlobal.Agent.OnConnected -= Agent_OnConnected;
            GameGlobal.Agent.OnDisconnected -= Agent_OnDisconnected;
            GameGlobal.OnPacketReceived -= OnPacketReceived;
        }

        private void Agent_OnConnected(object sender, Data.EventArguments.ConnectionStatusEventArgs e)
        {

        }

        private void OnPacketReceived(object sender, PacketEventArgs e)
        {
            
        }

        private void Agent_OnDisconnected(object sender, Data.EventArguments.ConnectionStatusEventArgs e)
        {
            
        }

    }
}
