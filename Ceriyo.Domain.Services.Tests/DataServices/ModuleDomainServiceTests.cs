using System;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.DataServices;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.Mapping;
using Ceriyo.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Ceriyo.Domain.Services.Tests.DataServices
{
    public class ModuleDomainServiceTests
    {
        private const string BaseDirectory = "./Modules/temp0/";
        private ModuleData _moduleData;
        private IDataService _dataService;
        private Mock<ILogger> _mockLogger;
        private IModuleDomainService _moduleDomainService;
        private IObjectMapper _objectMapper;

        [SetUp]
        public void SetUp()
        {
            _moduleData = new ModuleData();
            _mockLogger = new Mock<ILogger>();
            _dataService = new DataService(_mockLogger.Object);
            _objectMapper = new ObjectMapper();
            _objectMapper.Initialize();
            _moduleDomainService = new ModuleDomainService(_dataService, _objectMapper);

            _dataService.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(BaseDirectory))
            {
                Directory.Delete(BaseDirectory, true);
            }
        }

        [Test]
        public void ModuleDomainService_CreateModule_ShouldCreateProjectStructure()
        {
            _moduleDomainService.CreateModule("name", "tag", "resref");

            Assert.IsTrue(Directory.Exists(BaseDirectory));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Ability"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Animation"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Class"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Creature"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Dialog"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Item"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}ItemProperty"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}ItemType"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Placeable"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Script"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Skill"));
            Assert.IsTrue(Directory.Exists($"{BaseDirectory}Tileset"));
            Assert.IsTrue(File.Exists($"{BaseDirectory}Module.dat"));
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

            Assert.IsFalse(Directory.Exists(BaseDirectory));
        }

    }
}
