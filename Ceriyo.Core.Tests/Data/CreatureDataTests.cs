using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class CreatureDataTests
    {
        [Test]
        public void CreatureData_OnInstantiate_ShouldCreateGlobalID()
        {
            CreatureData data = new CreatureData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
