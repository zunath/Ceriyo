using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Infrastructure.WPF.Tests
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
