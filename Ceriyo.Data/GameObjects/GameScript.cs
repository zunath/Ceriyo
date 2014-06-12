using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.AvalonEdit.Document;

namespace Ceriyo.Data.GameObjects
{
    public class GameScript
    {
        public string Name { get; set; }
        public TextDocument ScriptDocument { get; set; }

        public GameScript()
        {
            this.Name = "";
            this.ScriptDocument = new TextDocument();
        }

        public GameScript(string name, string scriptText)
        {
            this.Name = name;
            this.ScriptDocument = new TextDocument(scriptText);
        }
    }
}
