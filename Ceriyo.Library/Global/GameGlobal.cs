using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Library.Network;
using Ceriyo.Data.Packets;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Settings;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Library.Global
{
    public static class GameGlobal
    {
        public static NetworkAgent Agent { get; set; }
        public static string Username { get; set; }
        public static object ScreenTransferData { get; set; }
        public static event EventHandler<PacketEventArgs> OnPacketReceived;
        public static GameSettings Settings { get; private set; }

        static GameGlobal()
        {
            if (EngineConstants.IsDebugEnabled)
            {
                Username = "zunath";
            }
        }

        public static void Initialize()
        {
            Settings = new GameSettings(); // TODO: Load from XML file
            GameGlobal.Agent = new NetworkAgent(NetworkAgentRoleEnum.Client, null, Settings.Port);
        }

        public static void ProcessPackets()
        {
            List<PacketBase> packets = GameGlobal.Agent.CheckForPackets();

            foreach (PacketBase packet in packets)
            {
                if (OnPacketReceived != null)
                {
                    OnPacketReceived(Agent, new PacketEventArgs(packet));
                }
            }
        }
    }
}
