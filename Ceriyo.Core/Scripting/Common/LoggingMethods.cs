using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Scripting.Common.Contracts;

namespace Ceriyo.Core.Scripting.Common
{
    public class LoggingMethods: ILoggingMethods
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
