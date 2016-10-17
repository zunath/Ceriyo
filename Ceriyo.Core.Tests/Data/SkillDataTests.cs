using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class SkillDataTests
    {
        [Test]
        public void SkillData_OnInstantiate_ShouldCreateGlobalID()
        {
            SkillData data = new SkillData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
