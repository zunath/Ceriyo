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
            new EnteringGameScreenPacket {IsRequest = true}.Send(NetDeliveryMethod.ReliableUnordered);
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            
        }

        protected override void CustomDestroy()
        {
            TextManager.RemoveText(LoadingText);
        }


    }
}
