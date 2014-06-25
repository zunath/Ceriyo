using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Library.ScriptEngine;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Library.Tests.ScriptEngine
{
    [TestClass]
    public class ScriptMethodTests
    {
        #region GetName Tests
        [TestMethod]
        public void GetName_IsEqual()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area("areaName", "areaTag", "areaResref", 2, 2, 1);

            string name = methods.GetName(area);

            Assert.AreEqual("areaName", name);
        }

        [TestMethod]
        public void GetName_NullNameIsBlank()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area(null, null, null, 2, 2, 1);

            string name = methods.GetName(area);

            Assert.AreEqual(string.Empty, name);
        }

        [TestMethod]
        public void GetName_NullObjectIsBlank()
        {
            ScriptMethods methods = new ScriptMethods();
            string name = methods.GetName(null);
            Assert.AreEqual(string.Empty, name);
        }

        #endregion

        #region GetTag Tests

        [TestMethod]
        public void GetTag_IsEqual()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area("areaName", "areaTag", "areaResref", 2, 2, 1);

            string tag = methods.GetTag(area);

            Assert.AreEqual("areaTag", tag);
        }

        [TestMethod]
        public void GetTag_NullNameIsBlank()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area(null, null, null, 2, 2, 1);
            string tag = methods.GetTag(area);
            Assert.AreEqual(string.Empty, tag);
        }

        [TestMethod]
        public void GetTag_NullObjectIsBlank()
        {
            ScriptMethods methods = new ScriptMethods();
            string tag = methods.GetTag(null);
            Assert.AreEqual(string.Empty, tag);
        }

        #endregion

        #region GetResref tests

        [TestMethod]
        public void GetResref_IsEqual()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area("areaName", "areaTag", "areaResref", 2, 2, 1);

            string resref = methods.GetResref(area);

            Assert.AreEqual("areaResref", resref);
        }

        [TestMethod]
        public void GetResref_NullNameIsBlank()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area(null, null, null, 2, 2, 1);
            string resref = methods.GetResref(area);
            Assert.AreEqual(string.Empty, resref);
        }

        [TestMethod]
        public void GetResref_NullObjectIsBlank()
        {
            ScriptMethods methods = new ScriptMethods();
            string resref = methods.GetResref(null);
            Assert.AreEqual(string.Empty, resref);
        }


        #endregion


    }
}
