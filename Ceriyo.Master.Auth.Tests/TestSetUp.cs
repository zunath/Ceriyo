using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Master.Auth.Tests
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
