using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.IO;

namespace Ceriyo.Data.GameObjects
{
    public class LevelChart
    {
        public List<LevelChartItem> Levels { get; set; }

        public LevelChart()
        {
            Levels = new List<LevelChartItem>();
        }
    }
}
