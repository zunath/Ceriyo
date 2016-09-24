using Artemis;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Scripting
{
    public class ScriptQueueObject
    {
        public string FilePath { get; set; }
        public string MethodName { get; set; }
        public ScriptEngine EngineType { get; set; }
        public Entity TargetObject { get; set; }

        public ScriptQueueObject(string filePath, 
            string methodName, 
            ScriptEngine engineType, 
            Entity targetObject)
        {
            FilePath = filePath;
            MethodName = methodName;
            EngineType = engineType;
            TargetObject = targetObject;
        }

    }
}
