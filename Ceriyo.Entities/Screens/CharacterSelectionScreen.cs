using System;
using System.Collections.Generic;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network.Packets;
using Lidgren.Network;

namespace Ceriyo.Entities.Screens
{
    public class CharacterSelectionScreen : BaseScreen
    {
        private CharacterSelectionMenuLogic GUI { get; set; }
        private List<Player> Players { get; set; }

        public CharacterSelectionScreen()
            : base("CharacterSelectionScreen")
        {
            Players = new List<Player>();
        }

        protected override void CustomInitialize()
        {
            SubscribePacketActions();

            CharacterSelectionScreenPacket packet = new CharacterSelectionScreenPacket
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
            GUI.OnCreateCharacter -= GUI_OnCreateCharacter;
            GUI.OnDeleteCharacter -= GUI_OnDeleteCharacter;
            GUI.OnDisconnected -= GUI_OnDisconnected;
            GUI.OnEnterServer -= GUI_OnEnterServer;
            GUI.Destroy();
        }

        private void SubscribePacketActions()
        {
            CeriyoServices.SubscribePacketAction(typeof(DeleteCharacterPacket), ReceiveDeleteCharacterPacket);
            CeriyoServices.SubscribePacketAction(typeof(CharacterSelectionScreenPacket), ReceiveCharacterSelectionScreenPacket);
            CeriyoServices.SubscribePacketAction(typeof(SelectCharacterPacket), ReceiveSelectCharacterPacket);
        }

        private void UnsubscribePacketActions()
        {
            CeriyoServices.UnsubscribePacketAction(typeof(DeleteCharacterPacket), ReceiveDeleteCharacterPacket);
            CeriyoServices.UnsubscribePacketAction(typeof(CharacterSelectionScreenPacket), ReceiveCharacterSelectionScreenPacket);
            CeriyoServices.UnsubscribePacketAction(typeof(SelectCharacterPacket), ReceiveSelectCharacterPacket);
        }

        #region Packet Processing

        private void ReceiveDeleteCharacterPacket(PacketBase packetBase)
        {
            DeleteCharacterPacket packet = (DeleteCharacterPacket)packetBase;
            if (packet.IsDeleteSuccessful)
            {
                GUI.PerformCharacterDelete();
            }
        }

        private void ReceiveCharacterSelectionScreenPacket(PacketBase packetBase)
        {
            CharacterSelectionScreenPacket packet = (CharacterSelectionScreenPacket)packetBase;

            Players = packet.CharacterList;

            GUI = new CharacterSelectionMenuLogic(packet.CanDeleteCharacters, Players);
            GUI.OnCreateCharacter += GUI_OnCreateCharacter;
            GUI.OnDeleteCharacter += GUI_OnDeleteCharacter;
            GUI.OnDisconnected += GUI_OnDisconnected;
            GUI.OnEnterServer += GUI_OnEnterServer;
        }

        private void ReceiveSelectCharacterPacket(PacketBase packetBase)
        {
            SelectCharacterPacket packet = (SelectCharacterPacket) packetBase;

            if (packet.IsSuccessful)
            {
                MoveToScreen(typeof(EnteringGameScreen));
            }
            else
            {
                // TODO: Display error message saying unable to select character.
            }
        }

        #endregion

        #region GUI Events

        private void GUI_OnEnterServer(object sender, ResrefEventArgs e)
        {
            SelectCharacterPacket packet = new SelectCharacterPacket
            {
                Resref = e.Resref
            };

            packet.Send(NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnDisconnected(object sender, EventArgs e)
        {
            CeriyoServices.Agent.Disconnect();
            MoveToScreen(typeof(MainMenuScreen));
        }

        private void GUI_OnDeleteCharacter(object sender, ResrefEventArgs e)
        {
            DeleteCharacterPacket packet = new DeleteCharacterPacket
            {
                IsRequest = true,
                CharacterResref = e.Resref
            };

            packet.Send(NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnCreateCharacter(object sender, EventArgs e)
        {
            MoveToScreen(typeof(CharacterCreationScreen));
        }

        #endregion
    }
}
