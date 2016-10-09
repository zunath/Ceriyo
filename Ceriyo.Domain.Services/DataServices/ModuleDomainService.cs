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
        private ModuleData _moduleData;
        private readonly IDataService _dataService;
        private readonly IObjectMapper _objectMapper;
        private readonly IModuleFactory _moduleFactory;
        private readonly IPathService _pathService;

        public ModuleDomainService(IDataService dataService,
            IObjectMapper objectMapper,
            IModuleFactory moduleFactory,
            IPathService pathService)
        {
            _objectMapper = objectMapper;
            _dataService = dataService;
            _moduleFactory = moduleFactory;
            _pathService = pathService;
        }

        public void CreateModule(string name,
            string tag,
            string resref)
        {
            _moduleData = _moduleFactory.Create();
            _moduleData.Name = name;
            _moduleData.Tag = tag;
            _moduleData.Resref = resref;
            
            CreateProjectStructure();
        }


        private void CreateProjectStructure()
        {
            if (Directory.Exists(_pathService.ModulesTempDirectory))
            {
                throw new Exception("An unsaved module already exists.");
            }

            Directory.CreateDirectory(_pathService.ModulesTempDirectory);
            CreateModuleDirectories();

            SaveModuleProperties();
        }

        private void LoadModuleProperties()
        {
            _moduleData = _dataService.Load<ModuleData>($"{_pathService.ModulesTempDirectory}Module.dat");
        }

        public void SaveModuleProperties()
        {
            _dataService.Save(_moduleData, $"{_pathService.ModulesTempDirectory}Module.dat");
        }

        public void CloseModule()
        {
            if (Directory.Exists(_pathService.ModulesTempDirectory))
            {
                Directory.Delete(_pathService.ModulesTempDirectory, true);
            }

            _moduleData = _moduleFactory.Create();
        }

        public void OpenModule(string fileName)
        {
            CloseModule();
            string filePath = $"{_pathService.ModuleDirectory}{fileName}.mod";
            _dataService.UnpackageDirectory(_pathService.ModulesTempDirectory, filePath);
            CreateModuleDirectories();
            LoadModuleProperties();
        }

        public void PackModule(string fileName)
        {
            _dataService.PackageDirectory(_pathService.ModulesTempDirectory, $"{_pathService.ModuleDirectory}{fileName}.mod");
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
            if (Directory.Exists($"{_pathService.ModulesTempDirectory}{directory}")) return;
            Directory.CreateDirectory($"{_pathService.ModulesTempDirectory}{directory}");
        }
    }
}
