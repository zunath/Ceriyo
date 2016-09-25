using Ceriyo.Core.Scripting.Client;
using Ceriyo.Core.UI.Controls;
using NUnit.Framework;
using Squid;

namespace Ceriyo.Core.Tests.Scripting.Client
{
    public class ControlMethodsTests
    {
        private ControlMethods _controlMethods;

        [SetUp]
        public void SetUp()
        {
            _controlMethods = new ControlMethods();
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void CreateWindow_NoTitle_ShouldBeWindow()
        {
            var window = _controlMethods.CreateWindow(10, 10, 10, 10, null, false);
            Assert.AreEqual(typeof(Window), window.GetType());
        }

        [Test]
        public void CreateWindow_HasTitle_ShouldBeTitledWindow()
        {
            var window = _controlMethods.CreateWindow(10, 10, 10, 10, "the title", false);
            Assert.AreEqual(typeof(TitledWindow), window.GetType());
        }

        [Test]
        public void CreateWindow_ConstructorArguments_ShouldMatch()
        {
            TitledWindow window = (TitledWindow)_controlMethods.CreateWindow(1, 2, 3, 4, "the title", false);

            Assert.AreEqual(window.Size.x, 1);
            Assert.AreEqual(window.Size.y, 2);
            Assert.AreEqual(window.Position.x, 3);
            Assert.AreEqual(window.Position.y, 4);
            Assert.AreEqual(window.Titlebar.Text, "the title");
            Assert.AreEqual(window.Resizable, false);
        }



    }
}
