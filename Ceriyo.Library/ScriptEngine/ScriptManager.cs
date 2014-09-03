using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using CSScriptLibrary;


namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptManager
    {
        private ScriptMethods Methods { get; set; }

        public ScriptManager()
        {
            this.Methods = new ScriptMethods();
        }

        public object RunEngineScript(string scriptName, object self)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (File.Exists(EnginePaths.ScriptsDirectory + scriptName))
            {
                string filePath = EnginePaths.ScriptsDirectory + scriptName;
                string contents = FileManager.FromFileText(filePath);

                // Load delegate method and call it.
                (new AsmHelper(CSScript.LoadMethod(contents)).GetStaticMethod("*.Main"))();

                return null; // DEBUG
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
                string filePath = EnginePaths.ScriptsDirectory + scriptName;
                string contents = FileManager.FromFileText(filePath);
                // Load delegate method and call it.
                (new AsmHelper(CSScript.LoadMethod(contents)).GetStaticMethod("*.Main"))();

                return null; // DEBUG
            }
            else
            {
                return null;
            }
        }

    }
}
