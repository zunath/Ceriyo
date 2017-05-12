using System;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Services.Module;
using Ceriyo.Infrastructure.Services;
using Ceriyo.Toolset.WPF.Mapping;
using Moq;
using NUnit.Framework;

namespace Ceriyo.Domain.Services.Tests.DataServices
{
    public class ModuleDomainServiceTests
    {
        private IDataService _dataService;
        private Mock<ILogger> _mockLogger;
        private IModuleService _moduleDomainService;
        private IObjectMapper _objectMapper;
        private IPathService _pathService;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _dataService = new DataService(_mockLogger.Object);

            _objectMapper = new ToolsetObjectMapper();
            _objectMapper.Initialize();
            _pathService = new PathService();
            _moduleDomainService = new ModuleService(_dataService, _objectMapper, _pathService, false);

            _dataService.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_pathService.ModulesToolsetTempDirectory))
            {
                Directory.Delete(_pathService.ModulesToolsetTempDirectory, true);
            }
        }

        [Test]
        public void ModuleDomainService_CreateModule_ShouldCreateProjectStructure()
        {
            _moduleDomainService.CreateModule("name", "tag", "resref");

            Assert.IsTrue(Directory.Exists(_pathService.ModulesToolsetTempDirectory));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Ability"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Animation"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Class"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Creature"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Dialog"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Item"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}ItemProperty"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}ItemType"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Placeable"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Script"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Skill"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesToolsetTempDirectory}Tileset"));
            Assert.IsTrue(File.Exists($"{_pathService.ModulesToolsetTempDirectory}Module.dat"));
        }

        [Test]
        public void ModuleDomainService_CreateModuleTwice_ShouldThrow()
        {
            _moduleDomainService.CreateModule("name1", "tag1", "resref1");

            Assert.Throws<Exception>(() =>
            {
                _moduleDomainService.CreateModule("name2", "tag2", "resref2");
            });
        }

        [Test]
        public void ModuleDomainService_CloseModule_ShouldDeleteBaseDirectory()
        {
            _moduleDomainService.CreateModule("name", "tag", "resref");
            _moduleDomainService.CloseModule();

            Assert.IsFalse(Directory.Exists(_pathService.ModulesToolsetTempDirectory));
        }

    }
}
