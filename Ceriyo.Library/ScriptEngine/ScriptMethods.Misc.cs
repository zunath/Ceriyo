using System;

namespace Ceriyo.Library.ScriptEngine
{
    public partial class ScriptMethods
    {
        [ScriptMethod]
        public void Print(string message)
        {
            Console.WriteLine(message);
        }

    }
}
