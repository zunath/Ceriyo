using System;
using Ceriyo.Core.Attributes;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Attributes
{
    public class ColorTypeAttributeTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void ColorTypeAttribute_Constructor_ValuesShouldMatch()
        {
            ColorTypeAttribute color = new ColorTypeAttribute(123, 222, 50, 25);

            Assert.AreEqual(123, color.Red);
            Assert.AreEqual(50, color.Blue);
            Assert.AreEqual(222, color.Green);
            Assert.AreEqual(25, color.Alpha);
        }

        [Test]
        public void ColorTypeAttribute_Red_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once UnusedVariable
                ColorTypeAttribute color = new ColorTypeAttribute(500, 0, 0, 0);
            });
        }

        [Test]
        public void ColorTypeAttribute_Green_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once UnusedVariable
                ColorTypeAttribute color = new ColorTypeAttribute(0, 500, 0, 0);
            });
        }

        [Test]
        public void ColorTypeAttribute_Blue_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once UnusedVariable
                ColorTypeAttribute color = new ColorTypeAttribute(0, 0, 500, 0);
            });
        }

        [Test]
        public void ColorTypeAttribute_Alpha_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once UnusedVariable
                ColorTypeAttribute color = new ColorTypeAttribute(0, 0, 0, 500);
            });
        }

    }
}
