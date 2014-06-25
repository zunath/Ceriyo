using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Toolset.ValidationRules;
using Ceriyo.Data;

namespace Ceriyo.Toolset.Tests.ValidationRules
{
    [TestClass]
    public class ResrefLengthRuleTests
    {
        [TestMethod]
        public void ResrefLengthRule_IsMinimumEqual()
        {
            ResrefLengthRule rule = new ResrefLengthRule();
            Assert.AreEqual(1, rule.MinimumLength);
        }

        [TestMethod]
        public void ResrefLengthRule_IsMaximumEqual()
        {
            ResrefLengthRule rule = new ResrefLengthRule();
            Assert.AreEqual(EngineConstants.ResrefMaxLength, rule.MaximumLength);
        }
    }
}
