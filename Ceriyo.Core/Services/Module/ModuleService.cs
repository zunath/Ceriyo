using System;
using System.Collections.Generic;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services.Module
{
    public class ModuleService : IModuleService
    {
        private ModuleData _moduleData;
        private readonly IDataService _dataService;
        private readonly IObjectMapper _objectMapper;
        private readonly IPathService _pathService;
        private readonly bool _isRunningAsServer;
        private readonly string _modulesDirectory;

        public ModuleService(IDataService dataService,
            IObjectMapper objectMapper,
            IPathService pathService,
            bool isRunningAsServer)
        {
            _objectMapper = objectMapper;
            _dataService = dataService;
            _pathService = pathService;
            _isRunningAsServer = isRunningAsServer;

            _modulesDirectory = isRunningAsServer ? 
                _pathService.ModulesServerTempDirectory : 
                _pathService.ModulesToolsetTempDirectory;
        }

        public void CreateModule(string name,
            string tag,
            string resref)
        {
            if(_isRunningAsServer)
                throw new NotSupportedException("Cannot create module when running in server mode.");

            _moduleData = new ModuleData
            {
                Name = name,
                Tag = tag,
                Resref = resref
            };

            CreateProjectStructure();


            for (int x = 1; x <= _moduleData.MaxLevel; x++)
            {
                _moduleData.LevelChart.Add(new ClassLevelData
                {
                    Level = x,
                    ExperienceRequired = x * 100
                });
            }
        }


        private void CreateProjectStructure()
        {
            if (Directory.Exists(_modulesDirectory))
            {
                throw new Exception("An unsaved module already exists.");
            }

            Directory.CreateDirectory(_modulesDirectory);
            CreateModuleDirectories();

            SaveModuleProperties();
        }

        private void LoadModuleProperties()
        {
            _moduleData = _dataService.Load<ModuleData>($"{_modulesDirectory}Module.dat");
        }

        public void SaveModuleProperties()
        {
            _dataService.Save(_moduleData, $"{_modulesDirectory}Module.dat");
        }

        public void CloseModule()
        {
            if (Directory.Exists(_modulesDirectory))
            {
                Directory.Delete(_modulesDirectory, true);
            }

            _moduleData = new ModuleData();
        }

        public void OpenModule(string fileName)
        {
            CloseModule();
            string filePath = $"{_pathService.ModuleDirectory}{fileName}.mod";
            _dataService.UnpackageDirectory(_modulesDirectory, filePath);
            CreateModuleDirectories();
            LoadModuleProperties();
        }

        public void PackModule(string fileName)
        {
            if(_isRunningAsServer)
                throw new NotSupportedException("Cannot pack module when running in server mode.");

            _dataService.PackageDirectory(_modulesDirectory, $"{_pathService.ModuleDirectory}{fileName}.mod");
        }

        public void ReplaceResourcePacks(IEnumerable<string> resourcePacks)
        {
            if(_isRunningAsServer)
                throw new NotSupportedException("Cannot replace resource packs when running in server mode.");

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
            _moduleData.LevelChart.Clear();
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
            CreateDirectoryIfNotExist("Area");
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
            if (Directory.Exists($"{_modulesDirectory}{directory}")) return;
            Directory.CreateDirectory($"{_modulesDirectory}{directory}");
        }
    }
}
