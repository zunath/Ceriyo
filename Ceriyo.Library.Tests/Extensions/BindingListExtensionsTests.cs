using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using Ceriyo.Library.Extensions;
using System.Linq;

namespace Ceriyo.Library.Tests
{
    [TestClass]
    public class BindingListExtensionsTests
    {
        [TestMethod]
        public void RemoveAll()
        {
            BindingList<int> list = new BindingList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);
            list.Add(10);

            list.RemoveAll(x => x == 5);

            Nullable<int> found = list.SingleOrDefault(x => x == 5);

            Assert.AreEqual(0, found);
        }
    }
}
