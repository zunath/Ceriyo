using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class ItemTypeDataTests
    {

        [Test]
        public void ItemTypeData_OnInstantiate_ShouldCreateGlobalID()
        {
            ItemTypeData data = new ItemTypeData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
