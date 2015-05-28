using Ceriyo.Data.GameObjects;
using Ceriyo.Library.ScriptEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ceriyo.Library.Tests.ScriptEngine
{
    public partial class ScriptMethodsTests
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

        #region GetLocalNumber tests

        [TestMethod]
        public void GetLocalNumber_NullObjectEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            double result = methods.GetLocalNumber(null, string.Empty);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetLocalNumber_ValueEquals()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            area.LocalVariables.Add(new LocalVariable("testvar", "10.5"));
            double result = methods.GetLocalNumber(area, "testvar");
            Assert.AreEqual(result, 10.5);
        }

        [TestMethod]
        public void GetLocalNumber_ValueDoesNotEqual()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            area.LocalVariables.Add(new LocalVariable("testvar", "10.5"));
            double result = methods.GetLocalNumber(area, "testvar");
            Assert.AreNotEqual(result, 123.45);
        }

        #endregion

        #region GetLocalString tests

        [TestMethod]
        public void GetLocalString_NullObjectEqualsEmptyString()
        {
            string result = BuildAreaScriptMethods().GetLocalString(null, "nullval");
            Assert.AreEqual(result, string.Empty);
        }

        [TestMethod]
        public void GetLocalString_NoValueSetEqualsEmptyString()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            string result = methods.GetLocalString(area, "testvar");
            Assert.AreEqual(result, string.Empty);
        }

        [TestMethod]
        public void GetLocalString_ValueEquals()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            area.LocalVariables.Add(new LocalVariable("testvar", "test variable value"));
            string result = methods.GetLocalString(area, "testvar");
            Assert.AreEqual(result, "test variable value");
        }

        [TestMethod]
        public void GetLocalString_ValueDoesNotEqual()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            area.LocalVariables.Add(new LocalVariable("testvar", "test variable value"));
            string result = methods.GetLocalString(area, "testvar");
            Assert.AreNotEqual(result, "NotTheRightValue");
        }

        #endregion

        #region GetLocalBoolean tests

        [TestMethod]
        public void GetLocalBoolean_NullObjectEqualsFalse()
        {
            ScriptMethods methods = new ScriptMethods();
            bool result = methods.GetLocalBoolean(null, "testvar");
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void GetLocalBoolean_NoValueEqualsFalse()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            bool result = methods.GetLocalBoolean(area, "testvar");
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void GetLocalBoolean_ValueEqualsTrue()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            area.LocalVariables.Add(new LocalVariable("testvar", "true"));
            bool result = methods.GetLocalBoolean(area, "testvar");
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void GetLocalBoolean_ValueEqualsFalse()
        {
            ScriptMethods methods = new ScriptMethods();
            Area area = new Area();
            area.LocalVariables.Add(new LocalVariable("testvar", "false"));
            bool result = methods.GetLocalBoolean(area, "testvar");
            Assert.AreEqual(result, false);
        }

        #endregion

    }
}
