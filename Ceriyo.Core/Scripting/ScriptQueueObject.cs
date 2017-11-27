using Artemis;

namespace Ceriyo.Core.Scripting
{
    /// <summary>
    /// Information used in script queueing.
    /// </summary>
    public class ScriptQueueObject
    {
        /// <summary>
        /// The file path of the script.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The name of the method to run.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// The target entity to run the method on.
        /// </summary>
        public Entity TargetObject { get; set; }

        /// <summary>
        /// Constructs a new ScriptQueueObject.
        /// </summary>
        /// <param name="filePath">The file path of the script.</param>
        /// <param name="methodName">The name of the method to run.</param>
        /// <param name="targetObject">The target entity to run the method on.</param>
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
