using System;
using System.Linq;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Library.ScriptEngine
{
    public partial class ScriptMethods
    {

        [ScriptMethod]
        public Area GetAreaByTag(string tag)
        {
            try
            {
                return _areas.FirstOrDefault(x => x.Tag == tag);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [ScriptMethod]
        public Area[] GetAreas()
        {
            return _areas.ToArray();
        }

        [ScriptMethod]
        public int GetAreaWidth(Area area)
        {
            try
            {
                return area.MapWidth;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [ScriptMethod]
        public int GetAreaHeight(Area area)
        {
            try
            {
                return area.MapHeight;
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
