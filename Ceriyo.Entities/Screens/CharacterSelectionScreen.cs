using System;
using System.Collections.Generic;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using Lidgren.Network;

namespace Ceriyo.Entities.Screens
{
    public class CharacterSelectionScreen : BaseScreen
    {
        private CharacterSelectionMenuLogic GUI { get; set; }
        private List<Player> Players { get; set; }
        private NetworkTransferData _transferData;

        public CharacterSelectionScreen()
            : base("CharacterSelectionScreen")
        {
            Players = new List<Player>();
            _transferData = new NetworkTransferData();
        }

        protected override void CustomInitialize()
        {
            CeriyoServices.OnPacketReceived += ReceivePacket;

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
            CeriyoServices.OnPacketReceived -= ReceivePacket;
            GUI.OnCreateCharacter -= GUI_OnCreateCharacter;
            GUI.OnDeleteCharacter -= GUI_OnDeleteCharacter;
            GUI.OnDisconnected -= GUI_OnDisconnected;
            GUI.OnEnterServer -= GUI_OnEnterServer;
            GUI.Destroy();
        }


        private void ReceivePacket(object sender, PacketEventArgs e)
        {
            _transferData = e.Packet.ClientReceive(_transferData);

            Type type = e.Packet.GetType();

            if (type == typeof(DeleteCharacterPacket))
            {
                ProcessDeleteCharacterResponse(e.Packet as DeleteCharacterPacket);
            }
            else if (type == typeof(CharacterSelectionScreenPacket))
            {
                ProcessCharacterSelectionScreenPacket(e.Packet as CharacterSelectionScreenPacket);
            }
            else if (type == typeof(SelectCharacterPacket))
            {
                ProcessSelectCharacterResponse(e.Packet as SelectCharacterPacket);
            }
        }

        #region Packet Processing

        private void ProcessDeleteCharacterResponse(DeleteCharacterPacket packet)
        {
            if (packet.IsDeleteSuccessful)
            {
                GUI.PerformCharacterDelete();
            }
        }

        private void ProcessCharacterSelectionScreenPacket(CharacterSelectionScreenPacket packet)
        {
            Players = packet.CharacterList;

            GUI = new CharacterSelectionMenuLogic(packet.CanDeleteCharacters, Players);
            GUI.OnCreateCharacter += GUI_OnCreateCharacter;
            GUI.OnDeleteCharacter += GUI_OnDeleteCharacter;
            GUI.OnDisconnected += GUI_OnDisconnected;
            GUI.OnEnterServer += GUI_OnEnterServer;
        }

        private void ProcessSelectCharacterResponse(SelectCharacterPacket packet)
        {
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
