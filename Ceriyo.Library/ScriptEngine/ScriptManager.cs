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

        public object RunEngineScript(string scriptName, object self)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (File.Exists(EnginePaths.ScriptsDirectory + scriptName))
            {
                string filePath = EnginePaths.ScriptsDirectory + scriptName;
                string script = File.ReadAllText(filePath) + " Main();";
                this.JSContext.SetParameter("self", self);
                object result = this.JSContext.Run(script);
                return result;
            }
            else
            {
                return null;
            }
        }

        public object RunModuleScript(string scriptName, object self)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (File.Exists(WorkingPaths.ScriptsDirectory + scriptName))
            {
                string script = FileManager.FromFileText(WorkingPaths.ScriptsDirectory + scriptName) + " Main();";
                this.JSContext.SetParameter("self", self);
                object result = this.JSContext.Run(script);
                return result;
            }
            else
            {
                return null;
            }
        }

        ~ScriptManager()
        {
            this.JSContext.Dispose();
        }

    }
}
