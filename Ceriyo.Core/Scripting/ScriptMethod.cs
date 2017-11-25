using System.Collections.Generic;

namespace Ceriyo.Core.Scripting
{
    public class ScriptMethod
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Returns { get; set; }
        public List<string> Parameters { get; set; }
        public string Prototype { get; set; }

        public ScriptMethod()
        {
            Parameters = new List<string>();
        }

    }
}
