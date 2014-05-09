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
            return gameObject.Name;
        }

        public string GetTag(IGameObject gameObject)
        {
            return gameObject.Tag;
        }

        public string GetResref(IGameObject gameObject)
        {
            return gameObject.Resref;
        }

        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
