using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Domain.Services.Tests
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
