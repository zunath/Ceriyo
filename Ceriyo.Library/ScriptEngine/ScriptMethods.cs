using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptMethods
    {
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

        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
