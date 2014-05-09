using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using Noesis.Javascript;

namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptManager
    {
        private string _engineScriptDirectory;
        private const string ScriptFileExtension = ".js";
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
        public object[] RunScript(string scriptName, object self, params string[] resultParameterName)
        {
            scriptName += ScriptFileExtension;
            object[] result = new string[resultParameterName.Length];
            string filePath = FileManager.RelativeDirectory + @"Content/" + _engineScriptDirectory + "/" + scriptName;
            string script = File.ReadAllText(filePath) + "Main();";
            this.JSContext.SetParameter("self", self);
            this.JSContext.Run(script);

            for (int x = 0; x < resultParameterName.Length; x++)
            {
                result[x] = this.JSContext.GetParameter(resultParameterName[x]);
            }

            return result;
        }

        /// <summary>
        /// Executes a script located in a game module.
        /// </summary>
        public object[] RunScript(GameModule module, object self, params string[] resultParameterName)
        {
            object[] result = new object[resultParameterName.Length];
            string script = ""; // TODO: Load script from module
            this.JSContext.SetParameter("self", self);
            this.JSContext.Run(script);

            for (int x = 0; x < resultParameterName.Length; x++)
            {
                result[x] = this.JSContext.GetParameter(resultParameterName[x]);
            }

            return result;
        }

        ~ScriptManager()
        {
            this.JSContext.Dispose();
        }

    }
}
