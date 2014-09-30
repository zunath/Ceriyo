using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;

namespace Ceriyo.Entities.Screens
{
    public class CharacterCreationScreen : BaseScreen
    {
        private CharacterCreationMenuLogic GUI { get; set; }

        public CharacterCreationScreen()
            : base("CharacterCreationScreen")
        {

        }

        protected override void CustomInitialize()
        {
            GUI = new CharacterCreationMenuLogic();

            GameGlobal.OnPacketReceived += GameGlobal_OnPacketReceived;

            CharacterCreationPacket packet = new CharacterCreationPacket
            {
                IsRequest = true
            };

            GameGlobal.Agent.SendPacket(packet, GameGlobal.Agent.Connections[0], Lidgren.Network.NetDeliveryMethod.ReliableUnordered);

        }

        private void GameGlobal_OnPacketReceived(object sender, PacketEventArgs e)
        {
            if (e.Packet.GetType() == typeof(CharacterCreationPacket))
            {
                GUI.LoadServerData(e.Packet as CharacterCreationPacket);
            }
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            
        }

        protected override void CustomDestroy()
        {
            GUI.Destroy();
        }
    }
}
