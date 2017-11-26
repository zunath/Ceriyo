using System;
using System.Collections.Generic;
using System.IO;
using Artemis;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Scripting;
using Ceriyo.Core.Scripting.Common.Contracts;
using Ceriyo.Core.Scripting.Server.Contracts;
using NLua;
using NLua.Exceptions;

namespace Ceriyo.Infrastructure.Services
{
    public class ScriptService: IScriptService
    {
        private readonly Lua _luaEngine;
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
            if (string.IsNullOrWhiteSpace(fileName)) return;

            string filePath = "./Scripts/" + fileName;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Script '" + fileName + "' could not be found.");
            }

            _scriptQueue.Enqueue(new ScriptQueueObject(filePath, methodName, entity));
        }

        public void ExecuteQueuedScripts()
        {
            while (_scriptQueue.Count > 0)
            {
                var script = _scriptQueue.Dequeue();
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

        private List<string> TableToEnumerationText(string luaTableName)
        {
            LuaTable lt = _luaEngine.GetTable(luaTableName);
            List<string> values = new List<string>();

            foreach (var value in lt.Values)
            {
                values.Add(luaTableName + "." + value);
            }

            return values;
        }

        public IEnumerable<string> GetRegisteredEnumerations()
        {
            List<string> enumerations = new List<string>();

            enumerations.AddRange(TableToEnumerationText("CharacterType"));
            enumerations.AddRange(TableToEnumerationText("CollisionType"));
            enumerations.AddRange(TableToEnumerationText("DirectionType"));
            enumerations.AddRange(TableToEnumerationText("GameButton"));
            enumerations.AddRange(TableToEnumerationText("Color"));

            enumerations.Sort();

            return enumerations;
        }

        private void SandboxEngines()
        {
            // Sandbox Lua
            _luaEngine.DoString("import = function() end");
        }

        private void RegisterCommonMethods()
        {
            // Lua
            _luaEngine["Logging"] = _loggingMethods;
        }

        private void RegisterServerMethods()
        {
            // Lua
            _luaEngine["Entity"] = _entityMethods;
            _luaEngine["LocalData"] = _localDataMethods;
            _luaEngine["Physics"] = _physicsMethods;
            _luaEngine["Scripting"] = _scriptingMethods;

        }

        private void RegisterClientMethods()
        {
        }

        private void RegisterServerEnumerations()
        {
            RegisterEnumeration("Color", typeof(ColorType));
        }

        private void RegisterClientEnumerations()
        {
            RegisterEnumeration("Cursor", typeof(CursorType));
        }


        private void RegisterEnumeration(string propertyName, Type enumType)
        {
            _luaEngine.NewTable(propertyName);
            LuaTable lt = (LuaTable)_luaEngine[propertyName];
            
            foreach (var val in Enum.GetValues(enumType))
            {
                lt[Enum.GetName(enumType, val)] = val;
            }
        }

        public string ValidateScript(string scriptText)
        {
            try
            {
                _luaEngine.DoString(scriptText);
                return string.Empty;
            }
            catch (LuaScriptException ex)
            {
                return ex.Message;
            }
        }


    }
}
