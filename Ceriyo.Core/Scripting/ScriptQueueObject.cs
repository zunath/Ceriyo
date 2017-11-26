using Artemis;

namespace Ceriyo.Core.Scripting
{
    public class ScriptQueueObject
    {
        public string FilePath { get; set; }
        public string MethodName { get; set; }
        public Entity TargetObject { get; set; }

        public ScriptQueueObject(string filePath, 
            string methodName, 
            Entity targetObject)
        {
            FilePath = filePath;
            MethodName = methodName;
            TargetObject = targetObject;
        }

    }
}
