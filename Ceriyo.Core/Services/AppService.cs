﻿using System.IO;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    public class AppService: IAppService
    {
        public void CreateAppDirectoryStructure()
        {
            CreateDirectoryIfNotExists("Modules");
            CreateDirectoryIfNotExists("ResourcePacks");
            CreateDirectoryIfNotExists("ServerVault");
            CreateDirectoryIfNotExists("Settings");
        }

        private void CreateDirectoryIfNotExists(string directoryName)
        {
            if (!Directory.Exists($"./{directoryName}/"))
            {
                Directory.CreateDirectory($"./{directoryName}/");
            }
        }
    }
}
