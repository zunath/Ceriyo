using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ceriyo.Library.ScriptEngine;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;

namespace Ceriyo.Library.Tests.ScriptEngine
{
    [TestClass]
    public partial class ScriptMethodsTests
    {
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

        [TestMethod]
        public void GetAreas_ShouldBeZero()
        {
            ScriptMethods methods = new ScriptMethods();
            int count = methods.GetAreas().Count();
            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        public void GetAreas_ShouldBe5()
        {
            Area area1 = new Area();
            Area area2 = new Area();
            Area area3 = new Area();
            Area area4 = new Area();
            Area area5 = new Area();

            ScriptMethods methods = BuildScriptMethods(area1, area2, area3, area4, area5);
            int count = methods.GetAreas().Count();
            Assert.AreEqual(count, 5);
        }

        [TestMethod]
        public void GetAreas_FirstMatches()
        {
            Area area1 = new Area("areaName1", "areaTag1", "areaResref1", 2, 2, 1);
            Area area2 = new Area("areaName2", "areaTag2", "areaResref2", 2, 2, 1);
            Area area3 = new Area("areaName3", "areaTag3", "areaResref3", 2, 2, 1);

            ScriptMethods methods = BuildScriptMethods(area1, area2, area3);
            Area[] areas = methods.GetAreas();
            Assert.AreSame(area1, areas[0]);
        }

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

        [TestMethod]
        public void GetAreaWidth_AreaWidthDoesNotEqual()
        {
            Area area = new Area("areaName", "areaTag", "areaResref", 25, 10, 1);
            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetAreaWidth(area);
            Assert.AreNotEqual(result, 300);
        }

        #endregion

        #region GetAreaHeight tests

        [TestMethod]
        public void GetAreaHeight_NullAreaEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetAreaHeight(null);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetAreaHeight_AreaHeightEquals()
        {
            Area area = new Area("areaName", "areaTag", "areaResref", 10, 25, 1);

            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetAreaHeight(area);
            Assert.AreEqual(result, 25);
        }

        [TestMethod]
        public void GetAreaHeight_AreaHeightDoesNotEqual()
        {
            Area area = new Area("areaName", "areaTag", "areaResref", 10, 25, 1);

            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetAreaHeight(area);
            Assert.AreNotEqual(result, 300);
        }

        #endregion

    }
}
