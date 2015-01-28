using ICSharpCode.AvalonEdit.Document;

namespace Ceriyo.Data.GameObjects
{
    public class GameScript
    {
        public string Name { get; set; }
        public TextDocument ScriptDocument { get; set; }

        public GameScript()
        {
            Name = string.Empty;
            ScriptDocument = new TextDocument();
        }

        public GameScript(string name, string scriptText)
        {
            Name = name;
            ScriptDocument = new TextDocument(scriptText);
        }
    }
}
