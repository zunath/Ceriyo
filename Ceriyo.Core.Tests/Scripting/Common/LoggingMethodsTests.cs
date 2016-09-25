using Ceriyo.Core.Scripting.Common;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting.Common
{
    public class LoggingMethodsTests
    {
        private LoggingMethods _loggingMethods;

        [SetUp]
        public void SetUp()
        {
            _loggingMethods = new LoggingMethods();
        }

        [Test]
        public void Print_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(
                delegate {
                    _loggingMethods.Print("Test");
                });
        }
    }
}
