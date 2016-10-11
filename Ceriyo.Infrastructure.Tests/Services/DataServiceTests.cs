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
        private ClassData _testClassData;
        private ItemTypeData _testItemTypeData;
        
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
            _testClassData = new ClassData
            {
                Name = "class 1",
                Tag = "class1",
                Resref = "class1"
            };

            _testItemTypeData = new ItemTypeData
            {
                Name = "item type 1",
                Resref = "itemtype1",
                Tag = "itemtype1"
            };
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

            _testClassData = null;
            _testItemTypeData = null;
        }

        [Test]
        public void PackageDirectory_RootFilesOnly_ShouldNotThrow()
        {
            // Set up
            _dataService.Save(_testClassData, TempDirectoryPath + _testClassData.Resref + ".dat");
            _dataService.Save(_testItemTypeData, TempDirectoryPath + _testItemTypeData.Resref + ".dat");
            
            // Act/Assert
            Assert.DoesNotThrow(() =>
            {
                _dataService.PackageDirectory(TempDirectoryPath, SerializedFilePath);
            });
            
            // Clean up
            File.Delete(TempDirectoryPath + _testClassData.Resref + ".dat");
            File.Delete(TempDirectoryPath + _testItemTypeData.Resref + ".dat");
        }

        [Test]
        public void PackageDirectory_WithSubDirectories_ShouldNotThrow()
        {
            // Set up
            _dataService.Save(_testClassData, TempDirectoryPath + "Classes/" + _testClassData.Resref + ".dat");
            _dataService.Save(_testItemTypeData, TempDirectoryPath + "ItemTypes/" + _testItemTypeData.Resref + ".dat");

            // Act/Assert
            Assert.DoesNotThrow(() =>
            {
                _dataService.PackageDirectory(TempDirectoryPath, SerializedFilePath);
            });

            // Clean up
            File.Delete(TempDirectoryPath + "Classes/" + _testClassData.Resref + ".dat");
            File.Delete(TempDirectoryPath + "ItemTypes/" + _testItemTypeData.Resref + ".dat");
            Directory.Delete(TempDirectoryPath + "Classes/");
            Directory.Delete(TempDirectoryPath + "ItemTypes/");
        }

        [Test]
        public void UnpackageDirectory_RootFilesOnly_ShouldNotThrow()
        {
            // Set up
            _dataService.Save(_testClassData, TempDirectoryPath + _testClassData.Resref + ".dat");
            _dataService.Save(_testItemTypeData, TempDirectoryPath + _testItemTypeData.Resref + ".dat");

            // Act/Assert
            _dataService.PackageDirectory(TempDirectoryPath, SerializedFilePath);
            Assert.DoesNotThrow(() =>
            {
                _dataService.UnpackageDirectory(TempDirectoryPath + "Output/", SerializedFilePath);
            });

            // Clean up
            File.Delete(TempDirectoryPath + _testClassData.Resref + ".dat");
            File.Delete(TempDirectoryPath + _testItemTypeData.Resref + ".dat");

            File.Delete(TempDirectoryPath + "output/" + _testClassData.Resref + ".dat");
            File.Delete(TempDirectoryPath + "output/" + _testItemTypeData.Resref + ".dat");
            Directory.Delete(TempDirectoryPath + "output/");
        }

        [Test]
        public void UnpackageDirectory_WithSubDirectoriesOnly_ShouldNotThrow()
        {
            // Set up
            _dataService.Save(_testClassData, TempDirectoryPath + "Classes/" + _testClassData.Resref + ".dat");
            _dataService.Save(_testItemTypeData, TempDirectoryPath + "ItemTypes/" + _testItemTypeData.Resref + ".dat");

            // Act/Assert
            _dataService.PackageDirectory(TempDirectoryPath, SerializedFilePath);
            Assert.DoesNotThrow(() =>
            {
                _dataService.UnpackageDirectory(TempDirectoryPath + "Output/", SerializedFilePath);
            });

            // Clean up
            File.Delete(TempDirectoryPath + "Classes/" + _testClassData.Resref + ".dat");
            File.Delete(TempDirectoryPath + "ItemTypes/" + _testItemTypeData.Resref + ".dat");
            File.Delete(TempDirectoryPath + "output/Classes/" + _testClassData.Resref + ".dat");
            File.Delete(TempDirectoryPath + "output/ItemTypes/" + _testItemTypeData.Resref + ".dat");

            Directory.Delete(TempDirectoryPath + "Classes/");
            Directory.Delete(TempDirectoryPath + "ItemTypes/");
            Directory.Delete(TempDirectoryPath + "output/Classes/");
            Directory.Delete(TempDirectoryPath + "output/ItemTypes/");

            Directory.Delete(TempDirectoryPath + "output/");
        }

        [Test]
        public void DataService_DeleteFile_ShouldDeleteFile()
        {
            const string path = "./testFile.dat";
            var stream = File.Create(path);
            stream.Close();

            _dataService.Delete(path);

            Assert.IsFalse(File.Exists(path));
        }
    }
}
