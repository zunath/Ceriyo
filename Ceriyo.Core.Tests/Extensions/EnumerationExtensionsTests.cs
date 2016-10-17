using System;
using Ceriyo.Core.Extensions;
using NUnit.Framework;
using q = System.ComponentModel;

namespace Ceriyo.Core.Tests.Extensions
{
    public class EnumerationExtensionsTests
    {
        private enum TestEnum
        {
            [q.Description("description of testval1")]
            TestVal1,
            [q.Description("description of testval2")]
            TestVal2
        }

        [Test]
        public void EnumerationExtensions_GetAttributeOfType_ShouldMatch()
        {
            const TestEnum val1 = TestEnum.TestVal1;
            const TestEnum val2 = TestEnum.TestVal2;

            q.DescriptionAttribute attr1 = val1.GetAttributeOfType<q.DescriptionAttribute>();
            q.DescriptionAttribute attr2 = val2.GetAttributeOfType<q.DescriptionAttribute>();

            Assert.AreEqual(attr1.Description, "description of testval1");
            Assert.AreEqual(attr2.Description, "description of testval2");


        }
    }
}
