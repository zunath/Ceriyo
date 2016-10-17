using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class AnimationDataTests
    {
        [Test]
        public void AnimationData_OnInstantiate_ShouldCreateGlobalID()
        {
            AnimationData data = new AnimationData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }
    }
}
