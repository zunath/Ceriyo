using System;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;

namespace Ceriyo.Domain.Services.DataServices
{
    public class ModuleDomainService: IModuleDomainService
    {
        const string BaseDirectory = "./temp0/";
        private readonly ModuleData _moduleData;
        private readonly IDataService _dataService;

        public ModuleDomainService(ModuleData moduleData,
            IDataService dataService)
        {
            _moduleData = moduleData;
            _dataService = dataService;
        }

        public void CreateModule(string name, 
            string tag, 
            string resref)
        {
            _moduleData.Name = name;
            _moduleData.Tag = tag;
            _moduleData.Resref = resref;

            CreateProjectStructure();
        }


        private void CreateProjectStructure()
        {
            if (Directory.Exists(BaseDirectory))
            {
                throw new Exception("An unsaved module already exists."); // TODO: Prompt user to remove unsaved module.
            }

            Directory.CreateDirectory(BaseDirectory);
            Directory.CreateDirectory($"{BaseDirectory}Ability");
            Directory.CreateDirectory($"{BaseDirectory}Animation");
            Directory.CreateDirectory($"{BaseDirectory}Class");
            Directory.CreateDirectory($"{BaseDirectory}Creature");
            Directory.CreateDirectory($"{BaseDirectory}Dialog");
            Directory.CreateDirectory($"{BaseDirectory}Item");
            Directory.CreateDirectory($"{BaseDirectory}ItemProperty");
            Directory.CreateDirectory($"{BaseDirectory}ItemType");
            Directory.CreateDirectory($"{BaseDirectory}Placeable");
            Directory.CreateDirectory($"{BaseDirectory}Script");
            Directory.CreateDirectory($"{BaseDirectory}Skill");
            Directory.CreateDirectory($"{BaseDirectory}Tileset");

            SaveModuleProperties();
        }

        public void SaveModuleProperties()
        {
            _dataService.Save(_moduleData, $"{BaseDirectory}Module.json");
        }
    }
}
