﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Toolset.ValidationRules;
using Ceriyo.Data;

namespace Ceriyo.Toolset.Tests.ValidationRules
{
    [TestClass]
    public class AreaWidthRuleTests
    {
        [TestMethod]
        public void AreaWidthRule_IsMinimumEqual()
        {
            AreaWidthRule rule = new AreaWidthRule();
            Assert.AreEqual(EngineConstants.AreaMinWidth, rule.Minimum);
        }

        [TestMethod]
        public void AreaWidthRule_IsMaximumEqual()
        {
            AreaWidthRule rule = new AreaWidthRule();
            Assert.AreEqual(EngineConstants.AreaMaxWidth, rule.Maximum);
        }
    }
}