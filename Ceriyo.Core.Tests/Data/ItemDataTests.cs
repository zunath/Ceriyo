using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class ItemDataTests
    {
        [Test]
        public void ItemData_OnInstantiate_ShouldCreateGlobalID()
        {
            ItemData data = new ItemData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
