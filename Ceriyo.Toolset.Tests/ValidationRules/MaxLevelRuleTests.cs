using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Toolset.ValidationRules;
using Ceriyo.Data;

namespace Ceriyo.Toolset.Tests.ValidationRules
{
    [TestClass]
    public class MaxLevelRuleTests
    {
        [TestMethod]
        public void MaxLevelRule_IsMinimumEqual()
        {
            MaxLevelRule rule = new MaxLevelRule();
            Assert.AreEqual(1, rule.Minimum);
        }

        [TestMethod]
        public void MaxLevelRule_IsMaximumEqual()
        {
            MaxLevelRule rule = new MaxLevelRule();
            Assert.AreEqual(EngineConstants.MaxLevel, rule.Maximum);
        }
    }
}
