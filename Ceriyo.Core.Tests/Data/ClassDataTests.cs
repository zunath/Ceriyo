using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class ClassDataTests
    {
        [Test]
        public void ClassData_OnInstantiate_ShouldCreateGlobalID()
        {
            ClassData data = new ClassData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
