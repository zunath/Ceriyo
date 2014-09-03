
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using System;
using System.Collections.Generic;
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
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
        }

        private void PacketReceived(object sender, PacketEventArgs e)
        {

        }

    }
}
