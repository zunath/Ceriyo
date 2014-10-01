﻿using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using FlatRedBall.Screens;
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
            GameGlobal.OnPacketReceived += PacketReceived;

            CharacterSelectionScreenPacket packet = new CharacterSelectionScreenPacket
            {
                IsRequest = true
            };

            GameGlobal.Agent.SendPacket(packet, GameGlobal.Agent.Connections[0], NetDeliveryMethod.ReliableUnordered);
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            GameGlobal.OnPacketReceived -= PacketReceived;
            GUI.OnCreateCharacter -= GUI_OnCreateCharacter;
            GUI.OnDeleteCharacter -= GUI_OnDeleteCharacter;
            GUI.OnDisconnected -= GUI_OnDisconnected;
            GUI.OnEnterServer -= GUI_OnEnterServer;
            GUI.Destroy();
        }


        private void PacketReceived(object sender, PacketEventArgs e)
        {
            Type type = e.Packet.GetType();

            if (type == typeof(DeleteCharacterPacket))
            {
                ProcessDeleteCharacterResponse(e.Packet as DeleteCharacterPacket);
            }
            else if (type == typeof(CharacterSelectionScreenPacket))
            {
                ProcessCharacterSelectionScreenPacket(e.Packet as CharacterSelectionScreenPacket);
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

        #endregion

        #region GUI Events

        private void GUI_OnEnterServer(object sender, EventArgs e)
        {
            
        }

        private void GUI_OnDisconnected(object sender, EventArgs e)
        {
            GameGlobal.Agent.Disconnect();
            MoveToScreen(typeof(MainMenuScreen));
        }

        private void GUI_OnDeleteCharacter(object sender, ResrefEventArgs e)
        {
            DeleteCharacterPacket packet = new DeleteCharacterPacket
            {
                IsRequest = true,
                CharacterResref = e.Resref
            };
            GameGlobal.Agent.SendPacket(packet, GameGlobal.Agent.Connections[0], Lidgren.Network.NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnCreateCharacter(object sender, EventArgs e)
        {
            MoveToScreen(typeof(CharacterCreationScreen));
        }

        #endregion
    }
}
