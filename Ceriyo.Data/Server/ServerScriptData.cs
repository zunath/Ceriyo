using System.Collections.Generic;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.Server
{
    public class ServerScriptData
    {
        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<Item> Items { get; set; } 

    }
}
