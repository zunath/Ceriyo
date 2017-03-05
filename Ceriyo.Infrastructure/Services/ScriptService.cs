using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Artemis;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Scripting;
using Ceriyo.Core.Scripting.Common.Contracts;
using Ceriyo.Core.Scripting.Server.Contracts;
using Jint;
using Jint.Runtime.Descriptors;
using NLua;

namespace Ceriyo.Infrastructure.Services
{
    public class ScriptService: IScriptService
    {
        private readonly Lua _luaEngine;
        private readonly Engine _javaScriptEngine;
        private readonly Queue<ScriptQueueObject> _scriptQueue;
        private readonly ILogger _logger;

        // Common Methods
        private readonly ILoggingMethods _loggingMethods;
        
        // Server Methods
        private readonly IEntityMethods _entityMethods;
        private readonly ILocalDataMethods _localDataMethods;
        private readonly IPhysicsMethods _physicsMethods;
        private readonly IScriptingMethods _scriptingMethods;

        public ScriptService(bool isServer,
            ILogger logger,
            ILoggingMethods loggingMethods,
            IEntityMethods entityMethods,
            ILocalDataMethods localDataMethods,
            IPhysicsMethods physicsMethods,
            IScriptingMethods scriptingMethods)
        {
            _logger = logger;

            _loggingMethods = loggingMethods;
            _entityMethods = entityMethods;
            _localDataMethods = localDataMethods;
            _physicsMethods = physicsMethods;
            _scriptingMethods = scriptingMethods;

            _javaScriptEngine = new Engine();
            _luaEngine = new Lua();
            _scriptQueue = new Queue<ScriptQueueObject>();

            SandboxEngines();
            RegisterCommonMethods();

            if (isServer)
            {
                RegisterServerMethods();
                RegisterServerEnumerations();
            }
            else
            {
                RegisterClientMethods();
                RegisterClientEnumerations();
            }

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
                    try
                    {
                        string text = File.ReadAllText(script.FilePath);
                        _javaScriptEngine.SetValue("self", script.TargetObject);
                        _javaScriptEngine.Execute(text);
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
                    try
                    {
                        _luaEngine["self"] = script.TargetObject;
                        _luaEngine.DoFile(script.FilePath);
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

        private void SandboxEngines()
        {
            // Sandbox Lua
            _luaEngine.DoString("import = function() end");

            // Sandbox JavaScript
            List<KeyValuePair<string, PropertyDescriptor>> methods = _javaScriptEngine.Global.GetOwnProperties().ToList();
            for (int index = methods.Count() - 1; index > 0; index--)
            {
                string propName = methods[index].Key;
                _javaScriptEngine.Global.RemoveOwnProperty(propName);
            }
        }

        private void RegisterCommonMethods()
        {
            // Lua
            _luaEngine["Logging"] = _loggingMethods;

            // JavaScript
            _javaScriptEngine.SetValue("Logging", _loggingMethods);
        }

        private void RegisterServerMethods()
        {
            // Lua
            _luaEngine["Entity"] = _entityMethods;
            _luaEngine["LocalData"] = _localDataMethods;
            _luaEngine["Physics"] = _physicsMethods;
            _luaEngine["Scripting"] = _scriptingMethods;

            // JavaScript
            _javaScriptEngine.SetValue("Entity", _entityMethods);
            _javaScriptEngine.SetValue("LocalData", _localDataMethods);
            _javaScriptEngine.SetValue("Physics", _physicsMethods);
            _javaScriptEngine.SetValue("Scripting", _scriptingMethods);

        }

        private void RegisterClientMethods()
        {
            //// Lua
            //_luaEngine["Control"] = _controlMethods;
            //_luaEngine["Style"] = _styleMethods;
            //_luaEngine["Scene"] = _sceneMethods;

            //// JavaScript
            //_javaScriptEngine.SetValue("Control", _controlMethods);
            //_javaScriptEngine.SetValue("Style", _styleMethods);
            //_javaScriptEngine.SetValue("Scene", _sceneMethods);
        }

        private void RegisterServerEnumerations()
        {
            RegisterEnumeration("Color", typeof(ColorType));
        }

        private void RegisterClientEnumerations()
        {
            //RegisterEnumeration("TextureMode", typeof(TextureMode));
            //RegisterEnumeration("Alignment", typeof(Alignment));
            RegisterEnumeration("Cursor", typeof(CursorType));
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
