using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Scripting.Common.Contracts;

namespace Ceriyo.Core.Scripting.Common
{
    /// <inheritdoc />
    [ScriptNamespace("Logging")]
    public class LoggingMethods: ILoggingMethods
    {
        /// <inheritdoc />
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
