using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Master.Server.Tests
{
    [SetUpFixture]
    public class TestSetUp
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestHelpers.SetUpEnvironmentDirectory();
        }
    }
}
