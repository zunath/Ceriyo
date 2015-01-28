using System.IO;
using System.Linq;
using System.Reflection;
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
            RegisterScriptMethods();
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
            _lua["this"] = self;

            return _lua.DoFile(filePath);
        }

        private void RegisterScriptMethods()
        {
            ScriptMethods methodObj = new ScriptMethods();
            MethodInfo[] methods = methodObj.GetType().GetMethods();

            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(typeof (LuaMethodAttribute), false).Any())
                {
                    _lua.RegisterFunction(method.Name, method);
                }
            }
        }

    }
}
