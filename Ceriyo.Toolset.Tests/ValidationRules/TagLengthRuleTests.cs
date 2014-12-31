using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Toolset.ValidationRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Ceriyo.Toolset.Tests.ValidationRules
{
    [TestClass]
    public class TagLengthRuleTests
    {
        [TestMethod]
        public void TagLengthRule_IsMinimumEqual()
        {
            TagLengthRule rule = new TagLengthRule();
            Assert.AreEqual(1, rule.MinimumLength);
        }

        [TestMethod]
        public void TagLengthRule_IsMaximumEqual()
        {
            TagLengthRule rule = new TagLengthRule();
            Assert.AreEqual(EngineConstants.TagMaxLength, rule.MaximumLength);
        }
    }
}
