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
using Newtonsoft.Json.Converters;
using NLua;

namespace Ceriyo.Infrastructure.Services
{
    public class ScriptService: IScriptService
    {
        private readonly Lua _luaEngine;
        private readonly Engine _javaScriptEngine;
        private readonly Queue<ScriptQueueObject> _scriptQueue;
        private readonly ILogger _logger;

        public ScriptService(bool isServer,
            ILogger logger)
        {
            _logger = logger;
            _javaScriptEngine = new Engine();
            _luaEngine = new Lua();
            _scriptQueue = new Queue<ScriptQueueObject>();

            if (isServer)
            {
                RegisterServerMethods();
                RegisterServerEnumerations();
            }
            else
            {
                RegisterClientMethods();
            }

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

                    try
                    {
                        _javaScriptEngine.Invoke(script.MethodName);
                    }
                    catch (Exception ex)
                    {
                        string fileName = Path.GetFileName(script.FilePath);
                        _logger.Error($"JavaScript error: {fileName}. Details: {ex.Message}");
                    }
                }
                else if (script.EngineType == ScriptEngine.Lua)
                {
                    _luaEngine["this"] = script.TargetObject;
                    _luaEngine.DoFile(script.FilePath);
                    try
                    {
                        ((LuaFunction)_luaEngine[script.MethodName]).Call();
                    }
                    catch (Exception ex)
                    {
                        string fileName = Path.GetFileName(script.FilePath);
                        _logger.Error($"Lua error: {fileName}. Details: {ex.Message}");
                    }
                }
            }
        }
        
        private void RegisterServerMethods()
        {
            var methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(p => typeof(IServerScriptMethodGroup).IsAssignableFrom(p))
                .SelectMany(m => m.GetMethods())
                .Where(p => p.DeclaringType != typeof(object))
                .ToList();
            RegisterMethods(methods);
        }

        private void RegisterMethods(List<MethodInfo> methods)
        {
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


        private void RegisterServerEnumerations()
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


        private void RegisterClientMethods()
        {
            var methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(p => typeof(IClientScriptMethodGroup).IsAssignableFrom(p))
                .SelectMany(m => m.GetMethods())
                .Where(p => p.DeclaringType != typeof(object))
                .ToList();
            RegisterMethods(methods);
        }


    }
}
