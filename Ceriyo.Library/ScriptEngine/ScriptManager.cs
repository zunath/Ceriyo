using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Server;
using NLua;

namespace Ceriyo.Library.ScriptEngine
{
    public static class ScriptManager
    {
        private static readonly Lua _lua;
        private static readonly ScriptMethods _scriptMethods;

        static ScriptManager()
        {
            _lua = new Lua();
            _scriptMethods = new ScriptMethods();
            SandboxVM();
            RegisterScriptMethods();
        }

        public static object[] RunEngineScript(string scriptName)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (!File.Exists(EnginePaths.ScriptsDirectory + scriptName)) return null;
            string filePath = EnginePaths.ScriptsDirectory + scriptName;

            _lua.DoFile(filePath);
            LuaFunction function = _lua["Main"] as LuaFunction;
            return function.Call();
        }

        public static object[] RunModuleScript(string scriptName, object self)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (!File.Exists(WorkingPaths.ScriptsDirectory + scriptName)) return null;
            string filePath = WorkingPaths.ScriptsDirectory + scriptName;
            _lua["this"] = self;
            object[] result = null;

            // We have to be a little looser with module scripts because the end user may have written bad code.
            try
            {
                _lua.DoFile(filePath);
                LuaFunction function = _lua["Main"] as LuaFunction;
                result = function.Call();
            }
            catch (Exception)
            {
                // TODO: Handle script errors. Pop up? Log?
            }

            return result;
        }

        private static void RegisterScriptMethods()
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

        private static void SandboxVM()
        {
            _lua.DoString("import = function() end");
            EnumerationToTable("ScriptEventType", typeof(ScriptEventType));
            EnumerationToTable("InventorySlot", typeof(InventorySlot));
            EnumerationToTable("GenderType", typeof(GenderType));
            EnumerationToTable("PVPType", typeof(PVPType));
            EnumerationToTable("GameType", typeof(GameType));
            EnumerationToTable("ItemPropertyType", typeof(ItemPropertyType));
        }

        private static void EnumerationToTable(string luaTableName, Type enumType)
        {
            _lua.NewTable(luaTableName);
            LuaTable lt = _lua[luaTableName] as LuaTable;

            foreach (var val in Enum.GetValues(enumType))
            {
                lt[Enum.GetName(enumType, val)] = (int)val;
            }
        }

        public static void Update(ServerScriptData data)
        {
            _scriptMethods.Update(ref data); // Don't copy the data again.
        }

    }
}
