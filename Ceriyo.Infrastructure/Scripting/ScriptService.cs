using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Artemis;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Scripting;
using Jint;
using Jint.Native;
using NLua;

namespace Ceriyo.Infrastructure.Scripting
{
    public class ScriptService: IScriptService
    {
        private readonly Lua _luaEngine;
        private readonly Engine _javaScriptEngine;
        private readonly Queue<ScriptQueueObject> _scriptQueue;

        public ScriptService()
        {
            _javaScriptEngine = new Engine();
            _luaEngine = new Lua();
            _scriptQueue = new Queue<ScriptQueueObject>();
            RegisterMethods();
            RegisterEnumerations();

            // Sandbox Lua
            _luaEngine.DoString("import = function() end");
        }

        public void QueueScript(string fileName, Entity entity, string methodName = "Main")
        {
            string filePath = "./Scripts/" + fileName;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Script '" + fileName + "' could not be found.");
            }
            string extension = Path.GetExtension(filePath);
            ScriptEngine engineType;
            switch (extension)
            {
                case ".js":
                    engineType = ScriptEngine.JavaScript;
                    break;
                case ".lua":
                    engineType = ScriptEngine.Lua;
                    break;
                default:
                    engineType = ScriptEngine.Unknown;
                    break;
            }

            if (engineType != ScriptEngine.Unknown)
            {
                _scriptQueue.Enqueue(new ScriptQueueObject(filePath, methodName, engineType, entity));
            }

        }

        public void ExecuteQueuedScripts()
        {
            while (_scriptQueue.Count > 0)
            {
                var script = _scriptQueue.Dequeue();
                

                if (script.EngineType == ScriptEngine.JavaScript)
                {
                    string text = File.ReadAllText(script.FilePath);
                    _javaScriptEngine.SetValue("self", script.TargetObject);
                    _javaScriptEngine.Execute(text);
                    _javaScriptEngine.Invoke(script.MethodName);
                }
                else if (script.EngineType == ScriptEngine.Lua)
                {
                    _luaEngine["this"] = script.TargetObject;
                    _luaEngine.DoFile(script.FilePath);
                    ((LuaFunction) _luaEngine[script.MethodName]).Call();
                }
            }
        }
        
        private void RegisterMethods()
        {
            var methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(p => typeof(IScriptMethodGroup).IsAssignableFrom(p))
                .SelectMany(m => m.GetMethods())
                .Where(p => p.DeclaringType != typeof(object))
                .ToList();

            foreach (var method in methods)
            {
                // Lua
                _luaEngine.RegisterFunction(method.Name, method);

                // JavaScript
                List<Type> args = new List<Type>(method.GetParameters().Select(p => p.ParameterType));
                Type delegateType;
                if (method.ReturnType == typeof(void))
                {
                    delegateType = Expression.GetActionType(args.ToArray());
                }
                else
                {
                    args.Add(method.ReturnType);
                    delegateType = Expression.GetFuncType(args.ToArray());
                }

                Delegate @delegate = Delegate.CreateDelegate(delegateType, null, method);
                _javaScriptEngine.SetValue(method.Name, @delegate);
            }
        }

        private void RegisterEnumerations()
        {
            RegisterEnumeration("Color", typeof(ColorType));
        }


        private void RegisterEnumeration(string propertyName, Type enumType)
        {
            _luaEngine.NewTable(propertyName);
            LuaTable lt = (LuaTable)_luaEngine[propertyName];

            var enumObject = new ExpandoObject() as IDictionary<string, object>;
            foreach (var val in Enum.GetValues(enumType))
            {
                lt[Enum.GetName(enumType, val)] = val;
                enumObject.Add(Enum.GetName(enumType, val), val);
            }
            _javaScriptEngine.SetValue(propertyName, enumObject);


        }
        


    }
}
