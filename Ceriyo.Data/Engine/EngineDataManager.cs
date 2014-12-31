using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ceriyo.Data.Engine
{
    public static class EngineDataManager
    {
        public static void InitializeEngine()
        {
            AddEngineDirectories();
        }

        private static void AddEngineDirectories()
        {
            if (!Directory.Exists(EnginePaths.ModulesDirectory))
            {
                Directory.CreateDirectory(EnginePaths.ModulesDirectory);
            }

            if (!Directory.Exists(EnginePaths.ScriptsDirectory))
            {
                Directory.CreateDirectory(EnginePaths.ScriptsDirectory);
            }

            if (!Directory.Exists(EnginePaths.ResourcePacksDirectory))
            {
                Directory.CreateDirectory(EnginePaths.ResourcePacksDirectory);
            }
        }

        public static List<Player> GetPlayers(string accountName)
        {
            List<Player> players = new List<Player>();

            string path = EnginePaths.CharactersDirectory + accountName;
            string[] files = Directory.GetFiles(path);

            foreach(string file in files)
            {
                Player player = FileManager.XmlDeserialize<Player>(file);
                players.Add(player);
            }

            return players;
        }

        public static Player GetPlayer(string username, string resref)
        {
            string path = EnginePaths.CharactersDirectory + username + "/" + resref + EnginePaths.DataExtension;
            return FileManager.XmlDeserialize<Player>(path);
        }

        public static bool SavePlayer(string accountName, Player pc, bool isNewPC = false)
        {
            bool success = false;

            try
            {
                string path = EnginePaths.CharactersDirectory + accountName + "/";
                if (isNewPC)
                {
                    pc.Resref = Guid.NewGuid().ToString();
                }

                path += pc.Resref + EnginePaths.DataExtension;

                FileManager.XmlSerialize(pc, path);

                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }

        public static bool DeletePlayer(string accountName, string resref)
        {
            bool success = false;

            try
            {
                string path = EnginePaths.CharactersDirectory + accountName + "/" + resref + EnginePaths.DataExtension;
                FileManager.DeleteFile(path);

                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }

    }
}
