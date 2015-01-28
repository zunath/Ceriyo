using System;
using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;

namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptMethods
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

        [ScriptMethod]
        public string GetName(IGameObject gameObject)
        {
            string result;
            try
            {
                result = gameObject.Name;
                result = result ?? string.Empty;
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        [ScriptMethod]
        public string GetTag(IGameObject gameObject)
        {
            string result;
            try
            {
                result = gameObject.Tag;
                result = result ?? string.Empty;
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        [ScriptMethod]
        public string GetResref(IGameObject gameObject)
        {
            string result;
            try
            {
                result = gameObject.Resref;
                result = result ?? string.Empty;
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        [ScriptMethod]
        public void Print(string message)
        {
            Console.WriteLine(message);
        }

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

        [ScriptMethod]
        public int GetLocalNumber(IGameObject gameObject, string variableName)
        {
            try
            {
                LocalVariable lv = gameObject.LocalVariables.SingleOrDefault(x => x.Name == variableName);
                return lv == null ? 0 : Convert.ToInt32(lv.Value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [ScriptMethod]
        public string GetLocalString(IGameObject gameObject, string variableName)
        {
            try
            {
                LocalVariable lv = gameObject.LocalVariables.SingleOrDefault(x => x.Name == variableName);
                return lv == null ? string.Empty : lv.Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [ScriptMethod]
        public bool GetLocalBoolean(IGameObject gameObject, string variableName)
        {
            try
            {
                LocalVariable lv = gameObject.LocalVariables.SingleOrDefault(x => x.Name == variableName);
                return lv != null && Convert.ToBoolean(lv.Value);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
