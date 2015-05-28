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

        public ScriptMethods()
        {
            _areas = new List<Area>();
            _items = new List<Item>();
        }

        public ScriptMethods(ServerScriptData data)
        {
            _areas = data.Areas.ToList();
            _items = data.Items.ToList();
        }

        public void Update(ref ServerScriptData data)
        {
            _areas = data.Areas.ToList();
            _items = data.Items.ToList();
        }

    }
}
