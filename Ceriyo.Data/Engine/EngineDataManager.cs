using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
    }
}
