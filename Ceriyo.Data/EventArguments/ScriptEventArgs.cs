using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class ScriptEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Contents { get; set; }

        public ScriptEventArgs(string name, string contents)
        {
            this.Name = name;
            this.Contents = contents;
        }
    }
}
