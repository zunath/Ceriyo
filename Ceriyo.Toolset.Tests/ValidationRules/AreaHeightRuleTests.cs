using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Toolset.ValidationRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ceriyo.Toolset.Tests
{
    [TestClass]
    public class AreaHeightRuleTests
    {
        [TestMethod]
        public void AreaHeightRule_IsMinimumEqual()
        {
            AreaHeightRule rule = new AreaHeightRule();
            Assert.AreEqual(EngineConstants.AreaMinHeight, rule.Minimum);
        }

        [TestMethod]
        public void AreaHeightRule_IsMaximumEqual()
        {
            AreaHeightRule rule = new AreaHeightRule();
            Assert.AreEqual(EngineConstants.AreaMaxHeight, rule.Maximum);
        }
    }
}
