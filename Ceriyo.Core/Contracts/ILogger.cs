using System;

namespace Ceriyo.Core.Contracts
{
    public interface ILogger
    {
        void Error(object message);
        void Error(object message, Exception ex);
        void Info(object message);
    }
}
