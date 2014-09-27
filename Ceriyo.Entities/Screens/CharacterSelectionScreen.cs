using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.NetworkObjects;
using FlatRedBall.Screens;

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
            UserConnectedPacket packet = GameGlobal.ScreenTransferData as UserConnectedPacket;

            if (EngineConstants.IsDebugEnabled)
            {
                if (packet == null)
                {
                    packet = new UserConnectedPacket
                    {
                        Announcement = "Test Announcement",
                        CanDeleteCharacters = false
                    };
                }
            }

            foreach (PlayerNO character in packet.CharacterList)
            {
                Players.Add(new Player
                {
                    Name = character.Name,
                    Description = character.Description
                });
            }

            GUI = new CharacterSelectionMenuLogic(packet.CanDeleteCharacters, Players);
            GUI.OnCreateCharacter += GUI_OnCreateCharacter;
            GUI.OnDeleteCharacter += GUI_OnDeleteCharacter;
            GUI.OnDisconnected += GUI_OnDisconnected;
            GUI.OnEnterServer += GUI_OnEnterServer;

            GameGlobal.ScreenTransferData = null;
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
            if (e.Packet.GetType() == typeof(DeleteCharacterPacket))
            {
                ProcessDeleteCharacterResponse(e.Packet as DeleteCharacterPacket);
            }
        }

        #region Packet Processing

        private void ProcessDeleteCharacterResponse(DeleteCharacterPacket packet)
        {

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

        private void GUI_OnDeleteCharacter(object sender, EventArgs e)
        {
            DeleteCharacterPacket packet = new DeleteCharacterPacket
            {
                IsRequest = true
            };
            GameGlobal.Agent.SendPacket(packet, GameGlobal.Agent.Connections[0], Lidgren.Network.NetDeliveryMethod.ReliableUnordered);
        }

        private void GUI_OnCreateCharacter(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
