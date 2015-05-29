using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Ceriyo.Library.ScriptEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ceriyo.Library.Tests.ScriptEngine
{
    public partial class ScriptMethodsTests
    {
        #region GetExperienceRequiredForLevel tests

        [TestMethod]
        public void GetExperienceRequiredForLevel_NegativeReturnsNegative1()
        {
            LevelChart chart = new LevelChart
            {
                Levels = new BindingList<LevelChartItem>()
            };
            chart.Levels.Add(new LevelChartItem(1, 100, 1, 1));
            chart.Levels.Add(new LevelChartItem(2, 200, 2, 2));
            chart.Levels.Add(new LevelChartItem(3, 300, 3, 3));
            chart.Levels.Add(new LevelChartItem(4, 400, 4, 4));
            chart.Levels.Add(new LevelChartItem(5, 500, 5, 5));
            chart.Levels.Add(new LevelChartItem(6, 600, 6, 6));

            ScriptMethods methods = new ScriptMethods(new ServerScriptData
            {
                Areas = new List<Area>(),
                Items = new List<Item>(),
                Levels = chart
            });
            int result = methods.GetExperienceRequiredForLevel(-20);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetExperienceRequiredForLevel_Matches()
        {
            LevelChart chart = new LevelChart
            {
                Levels = new BindingList<LevelChartItem>()
            };
            chart.Levels.Add(new LevelChartItem(1, 100, 1, 1));
            chart.Levels.Add(new LevelChartItem(2, 200, 2, 2));
            chart.Levels.Add(new LevelChartItem(3, 300, 3, 3));
            chart.Levels.Add(new LevelChartItem(4, 400, 4, 4));
            chart.Levels.Add(new LevelChartItem(5, 500, 5, 5));
            chart.Levels.Add(new LevelChartItem(6, 600, 6, 6));

            ScriptMethods methods = new ScriptMethods(new ServerScriptData
            {
                Areas = new List<Area>(),
                Items = new List<Item>(),
                Levels = chart
            });
            int result = methods.GetExperienceRequiredForLevel(3);
            Assert.AreEqual(result, 300);
        }


        [TestMethod]
        public void GetExperienceRequiredForLevel_DoesNotExistEqualsNegative1()
        {
            LevelChart chart = new LevelChart
            {
                Levels = new BindingList<LevelChartItem>()
            };
            chart.Levels.Add(new LevelChartItem(1, 100, 1, 1));
            chart.Levels.Add(new LevelChartItem(2, 200, 2, 2));
            chart.Levels.Add(new LevelChartItem(3, 300, 3, 3));
            chart.Levels.Add(new LevelChartItem(4, 400, 4, 4));
            chart.Levels.Add(new LevelChartItem(5, 500, 5, 5));
            chart.Levels.Add(new LevelChartItem(6, 600, 6, 6));

            ScriptMethods methods = new ScriptMethods(new ServerScriptData
            {
                Areas = new List<Area>(),
                Items = new List<Item>(),
                Levels = chart
            });
            int result = methods.GetExperienceRequiredForLevel(300);
            Assert.AreEqual(result, -1);
        }


        #endregion
    }
}
