using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class AreaDataTests
    {

        [Test]
        public void AreaData_OnInstantiate_ShouldCreateGlobalID()
        {
            AreaData data = new AreaData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
