using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using Noesis.Javascript;

namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptManager
    {
        private string _engineScriptDirectory;
        private const string JavascriptObjectName = "app";
        private const string JavascriptSelfName = "self";
        private JavascriptContext JSContext { get; set; }
        private ScriptMethods Methods { get; set; }

        public ScriptManager()
        {
            this.JSContext = new JavascriptContext();
            this.Methods = new ScriptMethods();
            this.JSContext.SetParameter("app", Methods);
            this._engineScriptDirectory = ConfigurationManager.AppSettings["GameFolder_EngineScripts"];
        }

        /// <summary>
        /// Executes a script from the engine scripts folder
        /// </summary>
        public object RunScript(string scriptName, object self)
        {
            scriptName += EnginePaths.ScriptExtension;
            string filePath = EnginePaths.ScriptsDirectory + scriptName;
            string script = File.ReadAllText(filePath) + "Main();";
            this.JSContext.SetParameter("self", self);
            object result = this.JSContext.Run(script);

            return result;
        }

        /// <summary>
        /// Executes a script located in a game module.
        /// </summary>
        public object RunScript(GameModule module, object self, params string[] resultParameterName)
        {
            string script = string.Empty; // TODO: Load script from module
            this.JSContext.SetParameter("self", self);
            object result = this.JSContext.Run(script);
            return result;
        }

        ~ScriptManager()
        {
            this.JSContext.Dispose();
        }

    }
}
