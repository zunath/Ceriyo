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
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using Lidgren.Network;

namespace Ceriyo.Library.Global
{
    public static class GameGlobal
    {
        public static NetworkAgent Agent { get; set; }
        public static string Username { get; set; }
        public static event EventHandler<PacketEventArgs> OnPacketReceived;
        public static GameSettings Settings { get; private set; }
        public static Player PC { get; set; }

        static GameGlobal()
        {
            if (EngineConstants.IsDebugEnabled)
            {
                Username = "zunath";
            }
        }

        public static void Initialize()
        {
            LoadGameSettings();
            GameGlobal.Agent = new NetworkAgent(NetworkAgentRoleEnum.Client, null, Settings.Port);
        }

        public static void SendPacket(PacketBase packet, NetDeliveryMethod deliveryMethod)
        {
            Agent.SendPacket(packet, Agent.Connections[0], deliveryMethod);
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

        private static void LoadGameSettings()
        {
            string path = EnginePaths.SettingsDirectory + "GameSettings" + EnginePaths.DataExtension;

            if (FileManager.FileExists(path))
            {
                Settings = FileManager.XmlDeserialize<GameSettings>(path);
            }
            else
            {
                Settings = new GameSettings();
                FileManager.XmlSerialize<GameSettings>(Settings, path);
            }
        }
    }
}
