using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using NLua;


namespace Ceriyo.Library.ScriptEngine
{
    public class ScriptManager
    {
        private ScriptMethods Methods { get; set; }
        private Lua LuaManager { get; set; }

        public ScriptManager()
        {
            this.Methods = new ScriptMethods();
            this.LuaManager = new Lua();
        }

        public object RunEngineScript(string scriptName)
        {
            scriptName += EnginePaths.ScriptExtension;
            if (File.Exists(EnginePaths.ScriptsDirectory + scriptName))
            {
                string filePath = EnginePaths.ScriptsDirectory + scriptName;
                string contents = FileManager.FromFileText(filePath);

                // Load delegate method and call it.
                //(new AsmHelper(CSScript.LoadMethod(contents)).GetStaticMethod("*.Main"))();
                //LuaManager.LoadCLRPackage();
                object[] test = LuaManager.DoFile(filePath);

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
                //(new AsmHelper(CSScript.LoadMethod(contents)).GetStaticMethod("*.Main"))();

                return null; // DEBUG
            }
            else
            {
                return null;
            }
        }

    }
}
