using System;

namespace Ceriyo.Data.EventArguments
{
    public class ScriptEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Contents { get; set; }
        public string OldName { get; set; }
        public bool IsOverwrite { get; set; }

        public ScriptEventArgs(string name, string contents, string oldName = "", bool isOverwrite = false)
        {
            Name = name;
            Contents = contents;
            OldName = oldName;
            IsOverwrite = isOverwrite;
        }
    }
}
