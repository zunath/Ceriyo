using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class ScriptDataTests
    {
        [Test]
        public void ScriptData_OnInstantiate_ShouldCreateGlobalID()
        {
            ScriptData data = new ScriptData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
