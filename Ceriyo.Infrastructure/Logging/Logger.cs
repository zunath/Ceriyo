using System;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        private readonly log4net.ILog _log;

        public Logger()
        {
            _log = log4net.LogManager.GetLogger(typeof(Logger));
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Error(object message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }
    }
}
