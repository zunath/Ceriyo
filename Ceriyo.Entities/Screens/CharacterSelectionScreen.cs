using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Library.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Entities.Screens
{
    public class CharacterSelectionScreen : BaseScreen
    {
        public CharacterSelectionScreen()
            : base("CharacterSelectionScreen")
        {
        }

        protected override void CustomInitialize()
        {
            GameGlobal.OnPacketReceived += PacketReceived;

            UserConnectedPacket packet = GameGlobal.ScreenTransferData as UserConnectedPacket;
            // TODO: Load data into character selection screen.
            GameGlobal.ScreenTransferData = null;

        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            GameGlobal.OnPacketReceived -= PacketReceived;
        }


        private void PacketReceived(object sender, PacketEventArgs e)
        {

        }
    }
}
