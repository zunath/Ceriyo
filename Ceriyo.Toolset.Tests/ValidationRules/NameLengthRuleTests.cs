using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Toolset.ValidationRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ceriyo.Toolset.Tests.ValidationRules
{
    [TestClass]
    public class NameLengthRuleTests
    {
        [TestMethod]
        public void NameLengthRule_IsMinimumEqual()
        {
            NameLengthRule rule = new NameLengthRule();
            Assert.AreEqual(1, rule.MinimumLength);
        }

        [TestMethod]
        public void MaxLevelRule_IsMaximumEqual()
        {
            NameLengthRule rule = new NameLengthRule();
            Assert.AreEqual(EngineConstants.NameMaxLength, rule.MaximumLength);
        }
    }
}
