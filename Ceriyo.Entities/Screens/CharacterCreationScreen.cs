﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using Ceriyo.Network;
using Ceriyo.Network.Packets;
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

            GameGlobal.OnPacketReceived += GameGlobal_OnPacketReceived;

            CharacterCreationScreenPacket packet = new CharacterCreationScreenPacket
            {
                IsRequest = true
            };

            GameGlobal.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnPlayButtonClicked(object sender, GameObjectEventArgs e)
        {
            CreateCharacterPacket packet = new CreateCharacterPacket
            {
                Description = e.GameObject.Description,
                Name = e.GameObject.Name
            };

            GameGlobal.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnCancelButtonClicked(object sender, EventArgs e)
        {
            MoveToScreen(typeof(CharacterSelectionScreen));
        }

        private void GameGlobal_OnPacketReceived(object sender, PacketEventArgs e)
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
            GameGlobal.PC = packet.ResponsePlayer;

            MoveToScreen(typeof(CharacterSelectionScreen));
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
