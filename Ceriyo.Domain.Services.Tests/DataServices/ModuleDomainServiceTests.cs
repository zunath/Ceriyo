using System;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.Mapping;
using Ceriyo.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Ceriyo.Domain.Services.Tests.DataServices
{
    public class ModuleDomainServiceTests
    {
        private IDataService _dataService;
        private Mock<ILogger> _mockLogger;
        private IModuleDomainService _moduleDomainService;
        private IObjectMapper _objectMapper;
        private IPathService _pathService;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _dataService = new DataService(_mockLogger.Object);
            _objectMapper = new ObjectMapper();
            _objectMapper.Initialize();
            _pathService = new PathService();
            _moduleDomainService = new ModuleDomainService(_dataService, _objectMapper, new ModuleFactory(new Mock<IValidatorFactory>().Object), _pathService);

            _dataService.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_pathService.ModulesTempDirectory))
            {
                Directory.Delete(_pathService.ModulesTempDirectory, true);
            }
        }

        [Test]
        public void ModuleDomainService_CreateModule_ShouldCreateProjectStructure()
        {
            _moduleDomainService.CreateModule("name", "tag", "resref");

            Assert.IsTrue(Directory.Exists(_pathService.ModulesTempDirectory));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Ability"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Animation"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Class"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Creature"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Dialog"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Item"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}ItemProperty"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}ItemType"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Placeable"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Script"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Skill"));
            Assert.IsTrue(Directory.Exists($"{_pathService.ModulesTempDirectory}Tileset"));
            Assert.IsTrue(File.Exists($"{_pathService.ModulesTempDirectory}Module.dat"));
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

            Assert.IsFalse(Directory.Exists(_pathService.ModulesTempDirectory));
        }

    }
}
