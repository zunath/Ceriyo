using System.IO;
using Ceriyo.Data;
using NLua;

namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptManager
    {
        private ScriptMethods Methods { get; set; }
        private Lua LuaManager { get; set; }

        public ScriptManager()
        {
            Methods = new ScriptMethods();
            LuaManager = new Lua();
        }

        public object[] RunEngineScript(string scriptName)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (!File.Exists(EnginePaths.ScriptsDirectory + scriptName)) return null;
            string filePath = EnginePaths.ScriptsDirectory + scriptName;
                
            return LuaManager.DoFile(filePath);
        }

        public object[] RunModuleScript(string scriptName, object self)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (!File.Exists(WorkingPaths.ScriptsDirectory + scriptName)) return null;
            string filePath = EnginePaths.ScriptsDirectory + scriptName;
                
            return LuaManager.DoFile(filePath);
        }

    }
}
