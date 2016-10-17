using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class DialogDataTests
    {
        [Test]
        public void DialogData_OnInstantiate_ShouldCreateGlobalID()
        {
            DialogData data = new DialogData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }

    }
}
