using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class PlaceableDataTests
    {
        [Test]
        public void PlaceableData_OnInstantiate_ShouldCreateGlobalID()
        {
            PlaceableData data = new PlaceableData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }

        [Test]
        public void PlaceableData_OnInstantiate_ShouldCreateLocalVariables()
        {
            PlaceableData data = new PlaceableData();
            Assert.IsNotNull(data.LocalVariables);
        }
    }
}
