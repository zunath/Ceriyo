using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Ceriyo.Infrastructure.Tests.Services
{
    public class DataServiceTests
    {
        private Mock<ILogger> _mockLogger;
        private IDataService _dataService;

        const string TempDirectoryPath = "./Testing/PackageDirectory/";
        const string SerializedFilePath = "./Testing/PackageDirectory/Serialized/Serialized.bin";

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _dataService = new DataService(_mockLogger.Object); 
            _dataService.Initialize();

            if (!Directory.Exists(TempDirectoryPath))
            {
                Directory.CreateDirectory(TempDirectoryPath);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(SerializedFilePath))
            {
                File.Delete(SerializedFilePath);
            }
            if (Directory.Exists("./Testing/PackageDirectory/Serialized"))
            {
                Directory.Delete("./Testing/PackageDirectory/Serialized");
            }
            if (Directory.Exists(TempDirectoryPath))
            {
                Directory.Delete(TempDirectoryPath);
            }
            if (Directory.Exists("./Testing"))
            {
                Directory.Delete("./Testing");
            }
        }

        [Test]
        public void PackageDirectory_RootFilesOnly_ShouldNotThrow()
        {
            // Set up
            ClassData classData = new ClassData
            {
                Name = "class 1",
                Tag = "class1",
                Resref = "class1"
            };

            ItemTypeData itemTypeData = new ItemTypeData
            {
                Name = "item type 1",
                Resref = "itemtype1",
                Tag = "itemtype1"
            };

            _dataService.Save(classData, TempDirectoryPath + classData.Resref + ".json");
            _dataService.Save(itemTypeData, TempDirectoryPath + itemTypeData.Resref + ".json");
            
            // Act/Assert
            Assert.DoesNotThrow(() =>
            {
                _dataService.PackageDirectory(TempDirectoryPath, SerializedFilePath);
            });
            
            // Clean up
            File.Delete(TempDirectoryPath + classData.Resref + ".json");
            File.Delete(TempDirectoryPath + itemTypeData.Resref + ".json");
        }

        [Test]
        public void PackageDirectory_WithSubDirectories_ShouldNotThrow()
        {
            // Set up
            ClassData classData = new ClassData
            {
                Name = "class 1",
                Tag = "class1",
                Resref = "class1"
            };

            ItemTypeData itemTypeData = new ItemTypeData
            {
                Name = "item type 1",
                Resref = "itemtype1",
                Tag = "itemtype1"
            };

            _dataService.Save(classData, TempDirectoryPath + "Classes/" + classData.Resref + ".json");
            _dataService.Save(itemTypeData, TempDirectoryPath + "ItemTypes/" + itemTypeData.Resref + ".json");

            // Act/Assert
            Assert.DoesNotThrow(() =>
            {
                _dataService.PackageDirectory(TempDirectoryPath, SerializedFilePath);
            });

            // Clean up
            File.Delete(TempDirectoryPath + "Classes/" + classData.Resref + ".json");
            File.Delete(TempDirectoryPath + "ItemTypes/" + itemTypeData.Resref + ".json");
            Directory.Delete(TempDirectoryPath + "Classes/");
            Directory.Delete(TempDirectoryPath + "ItemTypes/");
        }

    }
}
