using System.ComponentModel;
using System.Linq;
using Ceriyo.Library.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ceriyo.Library.Tests.Extensions
{
    [TestClass]
    public class BindingListExtensionsTests
    {
        [TestMethod]
        public void BindingListExtensions_RemoveAll()
        {
            BindingList<int> list = new BindingList<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            list.RemoveAll(x => x == 5);

            int? found = list.SingleOrDefault(x => x == 5);

            Assert.AreEqual(0, found);
        }
    }
}
