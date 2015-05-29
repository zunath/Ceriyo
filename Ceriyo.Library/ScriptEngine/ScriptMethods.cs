using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;

namespace Ceriyo.Library.ScriptEngine
{
    public partial class ScriptMethods
    {
        private List<Area> _areas;
        private List<Item> _items;
        private LevelChart _levelChart;

        public ScriptMethods()
        {
            _areas = new List<Area>();
            _items = new List<Item>();
            _levelChart = new LevelChart();
        }

        public ScriptMethods(ServerScriptData data)
        {
            _areas = data.Areas.ToList();
            _items = data.Items.ToList();
            _levelChart = data.Levels;
        }

        public void Update(ref ServerScriptData data)
        {
            _areas = data.Areas.ToList();
            _items = data.Items.ToList();
            _levelChart = data.Levels;

        }

    }
}
