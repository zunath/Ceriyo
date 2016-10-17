
using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class AbilityDataTests
    {
        [Test]
        public void AbilityData_OnInstantiate_ShouldCreateGlobalID()
        {
            AbilityData ability = new AbilityData();

            Assert.IsTrue(!string.IsNullOrWhiteSpace(ability.GlobalID));
        }
    }
}
