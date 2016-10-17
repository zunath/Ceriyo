using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class ItemPropertyDataTests
    {
        [Test]
        public void ItemPropertyData_OnInstantiate_ShouldCreateGlobalID()
        {
            ItemPropertyData data = new ItemPropertyData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
