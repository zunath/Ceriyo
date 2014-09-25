using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;

namespace Ceriyo.Data.Engine
{
    public class EngineDataManager
    {
        public void InitializeEngine()
        {
            AddEngineDirectories();
        }

        private void AddEngineDirectories()
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

        public List<Player> GetPlayers(string accountName)
        {
            List<Player> players = new List<Player>();

            try
            {
                string path = EnginePaths.CharactersDirectory + accountName;
                string[] files = Directory.GetFiles(path);

                foreach(string file in files)
                {
                    Player player = FileManager.XmlDeserialize<Player>(file);
                    players.Add(player);
                }
            }
            catch
            {
                throw;
            }

            return players;
        }

    }
}
