using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Ceriyo.Library.ScriptEngine;

namespace Ceriyo.Library.Tests.ScriptEngine
{
    public partial class ScriptMethodsTests
    {
        private ScriptMethods BuildAreaScriptMethods(params Area[] areaParams)
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
                Areas = areas,
                Items = new List<Item>()
            };

            return new ScriptMethods(data);
        }

        private ScriptMethods BuildItemScriptMethods(params Item[] itemParams)
        {
            List<Item> items = new List<Item>();

            if (!itemParams.Any())
            {
                Item item = new Item
                {
                    Name = "itemName",
                    Tag = "itemTag",
                    Resref = "itemResref"
                };
                items.Add(item);
            }
            else
            {
                items.AddRange(itemParams);
            }

            ServerScriptData data = new ServerScriptData
            {
                Areas = new List<Area>(),
                Items = items
            };

            return new ScriptMethods(data);
        }

    }
}
