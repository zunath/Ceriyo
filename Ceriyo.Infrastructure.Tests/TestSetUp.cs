using System;
using System.IO;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Infrastructure.Tests
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
