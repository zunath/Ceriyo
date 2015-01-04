using System;
using System.Collections.Generic;
using Ceriyo.Data.Enumerations;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;

namespace Ceriyo.Library.Global
{
    public static class CeriyoServices
    {
        public static NetworkAgent Agent { get; set; }
        public static event EventHandler<PacketEventArgs> OnPacketReceived;
        // public static GameSettings Settings { get; private set; }

        public static void Initialize(NetworkAgentRoleEnum networkRole, int port)
        {
            //LoadGameSettings();
            Agent = new NetworkAgent(networkRole, null, port);
        }

        public static void Update()
        {
            ProcessPackets();
        }

        private static void ProcessPackets()
        {
            List<PacketBase> packets = Agent.CheckForPackets();

            foreach (PacketBase packet in packets)
            {
                if (OnPacketReceived != null)
                {
                    OnPacketReceived(Agent, new PacketEventArgs(packet));
                }
            }
        }

        //private static void LoadGameSettings()
        //{
        //    string path = EnginePaths.SettingsDirectory + "GameSettings" + EnginePaths.DataExtension;

        //    if (FileManager.FileExists(path))
        //    {
        //        Settings = FileManager.XmlDeserialize<GameSettings>(path);
        //    }
        //    else
        //    {
        //        Settings = new GameSettings();
        //        FileManager.XmlSerialize(Settings, path);
        //    }
        //}
    }
}
