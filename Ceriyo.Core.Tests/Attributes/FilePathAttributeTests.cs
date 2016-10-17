using Ceriyo.Core.Attributes;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Attributes
{
    public class FilePathAttributeTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void FilePathAttribute_OnInstantiate_ShouldSetFilePath()
        {
            string filePath = "this is a fake file path";
            FilePathAttribute attr = new FilePathAttribute(filePath);
            Assert.AreEqual(attr.FilePath, filePath);
        }

    }
}
