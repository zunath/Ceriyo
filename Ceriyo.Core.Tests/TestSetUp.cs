using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests
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
