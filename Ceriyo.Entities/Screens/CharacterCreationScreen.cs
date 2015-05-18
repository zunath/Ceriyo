using System;
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.GUI;
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
            SubscribePacketActions();
            GUI = new CharacterCreationMenuLogic();
            GUI.OnPlayButtonClicked += GUI_OnPlayButtonClicked;
            GUI.OnCancelButtonClicked += GUI_OnCancelButtonClicked;

            CharacterCreationScreenPacket packet = new CharacterCreationScreenPacket
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
            UnsubscribePacketActions();
            GUI.Destroy();
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

        private void ReceiveCreateCharacterPacket(PacketBase packetBase)
        {
            // TODO: Store PC character in memory somewhere?

            MoveToScreen(typeof(CharacterSelectionScreen));
        }

        private void ReceiveCharacterCreationScreenPacket(PacketBase packetBase)
        {
            CharacterCreationScreenPacket packet = (CharacterCreationScreenPacket) packetBase;
            GUI.LoadServerData(packet);
        }

        private void SubscribePacketActions()
        {
            NetworkManager.SubscribePacketAction(typeof(CharacterCreationScreenPacket), ReceiveCharacterCreationScreenPacket);
            NetworkManager.SubscribePacketAction(typeof(CreateCharacterPacket), ReceiveCreateCharacterPacket);
        }

        private void UnsubscribePacketActions()
        {
            NetworkManager.UnsubscribePacketAction(typeof(CharacterCreationScreenPacket), ReceiveCharacterCreationScreenPacket);
            NetworkManager.UnsubscribePacketAction(typeof(CreateCharacterPacket), ReceiveCreateCharacterPacket);
        }
    }
}
