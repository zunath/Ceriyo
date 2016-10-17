using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class FrameDataTests
    {
        [Test]
        public void FrameData_OnInstantiate_ShouldCreateGlobalID()
        {
            FrameData data = new FrameData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
