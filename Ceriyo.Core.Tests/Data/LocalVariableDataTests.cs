using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class LocalVariableDataTests
    {
        [Test]
        public void LocalVariableData_OnInstantiate_ShouldCreateGlobalID()
        {
            LocalVariableData data = new LocalVariableData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }

        [Test]
        public void LocalVariableData_OnInstantia_ShouldCreateLocalStringsAndLocalDoubles()
        {
            LocalVariableData data = new LocalVariableData();
            Assert.IsNotNull(data.LocalStrings);
            Assert.IsNotNull(data.LocalDoubles);
        }

    }
}
