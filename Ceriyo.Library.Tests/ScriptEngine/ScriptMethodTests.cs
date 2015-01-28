using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Library.ScriptEngine;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;

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

        #region GetAreaByTag tests

        private ScriptMethods BuildScriptMethods(params Area[] areaParams)
        {
            List<Area> areas = new List<Area>();

            if (!areaParams.Any())
            {
                Area area = new Area("areaName", "areaTag", "areaResref", 10, 10, 1);
                areas.Add(area);
            }
            else
            {
                areas.AddRange(areaParams);
            }

            ServerScriptData data = new ServerScriptData
            {
                Areas = areas
            };

            return new ScriptMethods(data);
        }

        [TestMethod]
        public void GetAreaByTag_DoesNotExist()
        {
            ScriptMethods methods = BuildScriptMethods();
            Area area = methods.GetAreaByTag("xxx");
            Assert.IsNull(area);
        }

        [TestMethod]
        public void GetAreaByTag_Exists()
        {
            ScriptMethods methods = BuildScriptMethods();
            Area area = methods.GetAreaByTag("areaTag");
            Assert.IsNotNull(area);
        }

        [TestMethod]
        public void GetAreaByTag_FirstAreaShouldMatch()
        {
            Area area1 = new Area("area1Name", "area1Tag", "area1Resref", 0, 0, 0);
            Area area2 = new Area("area2Name", "area1Tag", "area2Resref", 0, 0, 0);

            ScriptMethods methods = BuildScriptMethods(area1, area2);

            Area area = methods.GetAreaByTag("area1Tag");
            Assert.AreSame(area1, area);
        }

        [TestMethod]
        public void GetAreaByTag_SecondAreaShouldNotMatch()
        {
            Area area1 = new Area("area1Name", "area1Tag", "area1Resref", 0, 0, 0);
            Area area2 = new Area("area2Name", "area1Tag", "area2Resref", 0, 0, 0);

            ScriptMethods methods = BuildScriptMethods(area1, area2);

            Area area = methods.GetAreaByTag("area1Tag");
            Assert.AreNotSame(area2, area);
        }

        #endregion

        #region GetAreas tests

        #endregion

        #region GetAreaWidth tests

        [TestMethod]
        public void GetAreaWidth_NullAreaEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetAreaWidth(null);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetAreaWidth_AreaWidthEquals()
        {
            Area area = new Area("areaName", "areaTag", "areaResref", 25, 10, 1);

            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetAreaWidth(area);
            Assert.AreEqual(result, 25);
        }

        #endregion

    }
}
