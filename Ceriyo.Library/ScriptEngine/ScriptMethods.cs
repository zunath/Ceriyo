using System;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptMethods
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
        public void Print(string message)
        {
            Console.WriteLine(message);
        }

    }
}
