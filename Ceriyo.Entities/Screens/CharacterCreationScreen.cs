using System;
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using Lidgren.Network;

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
            GUI.OnPlayButtonClicked += GUI_OnPlayButtonClicked;
            GUI.OnCancelButtonClicked += GUI_OnCancelButtonClicked;

            CeriyoServices.OnPacketReceived += ReceivePacket;

            CharacterCreationScreenPacket packet = new CharacterCreationScreenPacket
            {
                IsRequest = true
            };

            packet.Send(NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnPlayButtonClicked(object sender, GameObjectEventArgs e)
        {
            CreateCharacterPacket packet = new CreateCharacterPacket
            {
                Description = e.GameObject.Description,
                Name = e.GameObject.Name
            };

            packet.Send(NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnCancelButtonClicked(object sender, EventArgs e)
        {
            MoveToScreen(typeof(CharacterSelectionScreen));
        }

        private void ReceivePacket(object sender, PacketEventArgs e)
        {
            if (e.Packet.GetType() == typeof(CharacterCreationScreenPacket))
            {
                GUI.LoadServerData(e.Packet as CharacterCreationScreenPacket);
            }
            else if (e.Packet.GetType() == typeof(CreateCharacterPacket))
            {
                ReceiveCreateCharacterPacket(e.Packet as CreateCharacterPacket);
            }
        }

        private void ReceiveCreateCharacterPacket(CreateCharacterPacket packet)
        {
            // TODO: Store PC character in memory somewhere?

            MoveToScreen(typeof(CharacterSelectionScreen));
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            
        }

        protected override void CustomDestroy()
        {
            CeriyoServices.OnPacketReceived -= ReceivePacket;
            GUI.Destroy();
        }
    }
}
