using System;
using System.Linq;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Library.ScriptEngine
{
    public partial class ScriptMethods
    {
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
        public double GetLocalNumber(IGameObject gameObject, string variableName)
        {
            try
            {
                LocalVariable lv = gameObject.LocalVariables.SingleOrDefault(x => x.Name == variableName);
                return lv == null ? 0 : Convert.ToDouble(lv.Value);
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
