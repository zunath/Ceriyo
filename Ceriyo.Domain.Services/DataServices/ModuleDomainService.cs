using System;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class ModuleDomainService : IModuleDomainService
    {
        private const string BaseDirectory = "./Modules/temp0/";
        private readonly ModuleData _moduleData;
        private readonly IDataService _dataService;
        private readonly IObjectMapper _objectMapper;
        

        public ModuleDomainService(ModuleData moduleData,
            IDataService dataService,
            IObjectMapper objectMapper)
        {
            _moduleData = moduleData;
            _dataService = dataService;
            _objectMapper = objectMapper;
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
                throw new Exception("An unsaved module already exists.");
            }

            Directory.CreateDirectory(BaseDirectory);
            CreateModuleDirectories();

            SaveModuleProperties();
        }

        private void LoadModuleProperties()
        {
            ModuleData moduleData = _dataService.Load<ModuleData>($"{BaseDirectory}Module.json");
            _objectMapper.Map(moduleData, _moduleData);
        }

        public void SaveModuleProperties()
        {
            _dataService.Save(_moduleData, $"{BaseDirectory}Module.json");
        }

        public void CloseModule()
        {
            if (Directory.Exists(BaseDirectory))
            {
                Directory.Delete(BaseDirectory, true);
            }

            ModuleData moduleData = new ModuleData();
            _objectMapper.Map(moduleData, _moduleData);
        }

        public void OpenModule(string fileName)
        {
            CloseModule();
            string filePath = $"./Modules/{fileName}.mod";
            _dataService.UnpackageDirectory(BaseDirectory, filePath);
            CreateModuleDirectories();
            LoadModuleProperties();
        }

        public void PackModule(string fileName)
        {
            _dataService.PackageDirectory(BaseDirectory, $"./Modules/{fileName}.mod");
        }

        private void CreateModuleDirectories()
        {
            CreateDirectoryIfNotExist("Ability");
            CreateDirectoryIfNotExist("Animation");
            CreateDirectoryIfNotExist("Class");
            CreateDirectoryIfNotExist("Creature");
            CreateDirectoryIfNotExist("Dialog");
            CreateDirectoryIfNotExist("Item");
            CreateDirectoryIfNotExist("ItemProperty");
            CreateDirectoryIfNotExist("ItemType");
            CreateDirectoryIfNotExist("Placeable");
            CreateDirectoryIfNotExist("Script");
            CreateDirectoryIfNotExist("Skill");
            CreateDirectoryIfNotExist("Tileset");
        }

        private void CreateDirectoryIfNotExist(string directory)
        {
            if (Directory.Exists($"{BaseDirectory}{directory}")) return;
            Directory.CreateDirectory($"{BaseDirectory}{directory}");
        }
    }
}
