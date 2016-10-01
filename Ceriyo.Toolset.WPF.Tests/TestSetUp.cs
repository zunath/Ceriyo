using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Toolset.WPF.Tests
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
