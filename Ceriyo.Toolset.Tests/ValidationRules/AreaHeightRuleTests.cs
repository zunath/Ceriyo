﻿using Ceriyo.Data;
using Ceriyo.Toolset.ValidationRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
