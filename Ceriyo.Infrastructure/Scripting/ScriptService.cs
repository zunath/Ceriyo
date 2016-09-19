using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Artemis;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using NLua;

namespace Ceriyo.Infrastructure.Scripting
{
    public class ScriptService: IScriptService
    {
        private readonly Lua _lua;
        private readonly Queue<Tuple<string, Entity, string>> _scriptQueue;

        public ScriptService()
        {
            _scriptQueue = new Queue<Tuple<string, Entity, string>>();
            _lua = new Lua();
            SandboxVM();
            RegisterScriptMethods();
        }

        public void QueueScript(string fileName, Entity entity, string methodName = "Main")
        {
            if (!File.Exists("./Scripts/" + fileName))
            {
                throw new FileNotFoundException("Script '" + fileName + "' could not be found.");
            }

            _scriptQueue.Enqueue(new Tuple<string, Entity, string>(fileName, entity, methodName));
        }

        public void ExecuteQueuedScripts()
        {
            while (_scriptQueue.Count > 0)
            {
                var script = _scriptQueue.Dequeue();
                _lua["this"] = script.Item2;
                _lua.DoFile("./Scripts/" + script.Item1);

                if (_lua.GetFunction(script.Item3) != null)
                {
                    LuaFunction function = (LuaFunction)_lua[script.Item3];
                    function.Call();
                }
            }
        }


        private void SandboxVM()
        {
            _lua.DoString("import = function() end");
            EnumerationToTable("Color", typeof(ColorType));
        }

        private void EnumerationToTable(string luaTableName, Type enumType)
        {
            _lua.NewTable(luaTableName);
            LuaTable lt = (LuaTable)_lua[luaTableName];

            foreach (var val in Enum.GetValues(enumType))
            {
                lt[Enum.GetName(enumType, val)] = val;
            }
        }

        private void RegisterScriptMethods()
        {
            var methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(p => typeof(IScriptMethodGroup).IsAssignableFrom(p))
                .SelectMany(m => m.GetMethods())
                .Where(p => p.DeclaringType != typeof(object))
                .ToList();

            foreach (MethodInfo method in methods)
            {
                _lua.RegisterFunction(method.Name, method);
            }
        }
    }
}
