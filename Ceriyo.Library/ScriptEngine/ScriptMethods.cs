using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;

namespace Ceriyo.Library.ScriptEngine
{
    public partial class ScriptMethods
    {
        private List<Area> _areas;

        public ScriptMethods()
        {
            _areas = new List<Area>();
        }

        public ScriptMethods(ServerScriptData data)
        {
            _areas = data.Areas.ToList();
        }

        public void Update(ref ServerScriptData data)
        {
            _areas = data.Areas.ToList();
        }

    }
}
