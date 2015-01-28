using System;
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
        private readonly ScriptMethods _scriptMethods;

        public ScriptManager()
        {
            _lua = new Lua();
            _scriptMethods = new ScriptMethods();
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
            string filePath = WorkingPaths.ScriptsDirectory + scriptName;
            _lua["this"] = self;
            object[] result = null;

            // We have to be a little looser with module scripts because the end user may have written bad code.
            try
            {
                result = _lua.DoFile(filePath);
            }
            catch (Exception)
            {
                // TODO: Handle script errors. Pop up? Log?
            }

            return result;
        }

        private void RegisterScriptMethods()
        {
            MethodInfo[] methods = _scriptMethods.GetType().GetMethods();

            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(typeof (ScriptMethodAttribute), false).Any())
                {
                    _lua.RegisterFunction(method.Name, _scriptMethods, method);
                    
                }
            }
        }

    }
}
