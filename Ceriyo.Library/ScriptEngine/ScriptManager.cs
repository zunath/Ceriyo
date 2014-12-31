using System.IO;
using Ceriyo.Data.Engine;
using NLua;

namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptManager
    {
        private readonly Lua _lua;

        public ScriptManager()
        {
            _lua = new Lua();
            _lua["game"] = new ScriptMethods();
        }

        public object[] RunEngineScript(string scriptName)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (!File.Exists(EnginePaths.ScriptsDirectory + scriptName)) return null;
            string filePath = EnginePaths.ScriptsDirectory + scriptName;

            return _lua.DoFile(filePath);
        }

        public object[] RunModuleScript(string scriptName, object self)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (!File.Exists(WorkingPaths.ScriptsDirectory + scriptName)) return null;
            string filePath = EnginePaths.ScriptsDirectory + scriptName;

            return _lua.DoFile(filePath);
        }

    }
}
