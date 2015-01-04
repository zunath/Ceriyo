using System;
using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.Enumerations;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using FlatRedBall.Network;

namespace Ceriyo.Library.Global
{

    public delegate void PacketAction(PacketBase packet);
        
    public static class CeriyoServices
    {
        public static NetworkAgent Agent { get; set; }
        public static event EventHandler<PacketEventArgs> OnPacketReceived;
        // public static GameSettings Settings { get; private set; }

        private static List<Tuple<Type, PacketAction>> PacketActions; 

        public static void Initialize(NetworkAgentRoleEnum networkRole, int port)
        {
            //LoadGameSettings();
            Agent = new NetworkAgent(networkRole, null, port);
            PacketActions = new List<Tuple<Type, PacketAction>>();
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
                var actions = PacketActions.Where(x => x.Item1 == packet.GetType()).ToList();

                foreach (var action in actions)
                {
                    action.Item2(packet);
                }

                if (OnPacketReceived != null)
                {
                    OnPacketReceived(Agent, new PacketEventArgs(packet));
                }
            }
        }

        public static void SubscribePacketAction(Type packetType, PacketAction action)
        {
            PacketActions.Add(new Tuple<Type, PacketAction>(packetType, action));
        }

        public static void UnsubscribePacketAction(Type packetType, PacketAction action)
        {
            PacketActions.RemoveAll(x => x.Item1 == packetType && x.Item2 == action);
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
