using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class TilesetDataTests
    {
        [Test]
        public void TilesetData_OnInstantiate_ShouldCreateGlobalID()
        {
            TilesetData data = new TilesetData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
