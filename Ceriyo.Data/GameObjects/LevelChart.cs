using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FlatRedBall.IO;

namespace Ceriyo.Data.GameObjects
{
    public class LevelChart
    {
        public BindingList<LevelChartItem> Levels { get; set; }

        public LevelChart()
        {
            this.Levels = new BindingList<LevelChartItem>();
        }

        public LevelChart(BindingList<LevelChartItem> levels)
        {
            this.Levels = levels;
        }
    }
}
