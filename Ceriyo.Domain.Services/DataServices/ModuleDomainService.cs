using System;
using System.Collections.Generic;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class ModuleDomainService : IModuleDomainService
    {
        private const string BaseDirectory = "./Modules/temp0/";
        private ModuleData _moduleData;
        private readonly IDataService _dataService;
        private readonly IObjectMapper _objectMapper;

        public ModuleDomainService(IDataService dataService,
            IObjectMapper objectMapper)
        {
            _objectMapper = objectMapper;
            _dataService = dataService;
        }

        public void CreateModule(string name,
            string tag,
            string resref)
        {
            CloseModule();

            _moduleData = new ModuleData
            {
                Name = name,
                Tag = tag,
                Resref = resref
            };
            
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
            _moduleData = _dataService.Load<ModuleData>($"{BaseDirectory}Module.dat");
        }

        public void SaveModuleProperties()
        {
            _dataService.Save(_moduleData, $"{BaseDirectory}Module.dat");
        }

        public void CloseModule()
        {
            if (Directory.Exists(BaseDirectory))
            {
                Directory.Delete(BaseDirectory, true);
            }

            _moduleData = new ModuleData();
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

        public void ReplaceResourcePacks(IEnumerable<string> resourcePacks)
        {
            _moduleData.ResourcePacks.Clear();

            foreach (var pack in resourcePacks)
            {
                _moduleData.ResourcePacks.Add(pack);
            }
            SaveModuleProperties();

            BuildModule();
        }

        public void UpdateLoadedModuleData(ModuleData moduleData)
        {
            _objectMapper.Map(moduleData, _moduleData);
        }

        public ModuleData GetLoadedModuleData()
        {
            return _objectMapper.Map<ModuleData>(_moduleData);
        }
        
        public string GetLoadedModuleName()
        {
            return _moduleData.Name;
        }

        private void BuildModule()
        {
            
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
