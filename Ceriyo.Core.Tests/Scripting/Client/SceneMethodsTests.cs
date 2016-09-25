using Ceriyo.Core.Scripting.Client;
using Ceriyo.Core.Services.Contracts;
using Moq;
using NUnit.Framework;
using Squid;

namespace Ceriyo.Core.Tests.Scripting.Client
{
    public class SceneMethodsTests
    {
        private SceneMethods _sceneMethods;

        [SetUp]
        public void SetUp()
        {
            Mock<IUIService> mockUIService = new Mock<IUIService>();
            _sceneMethods = new SceneMethods(mockUIService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void CreateScene_ShouldBeDesktop()
        {
            var desktop = _sceneMethods.CreateScene();

            Assert.AreEqual(desktop.GetType(), typeof(Desktop));
        }

        [Test]
        public void AddControlToScene_Parent_ShouldMatch()
        {
            var desktop = _sceneMethods.CreateScene();
            Control control = new Control();
            _sceneMethods.AddControlToScene(desktop, control);
            Assert.AreEqual(control.Parent, desktop);
        }

        [Test]
        public void RemoveControlFromScene_Control_ShouldBeRemoved()
        {
            var desktop = _sceneMethods.CreateScene();
            Control control = new Control();
            _sceneMethods.AddControlToScene(desktop, control);
            _sceneMethods.RemoveControlFromScene(desktop, control);

            Assert.IsFalse(desktop.Controls.Contains(control));
        }

        [Test]
        public void RemoveControlFromScene_RemoveTwice_ShouldNotThrow()
        {
            var desktop = _sceneMethods.CreateScene();
            Control control = new Control();
            _sceneMethods.AddControlToScene(desktop, control);
            _sceneMethods.RemoveControlFromScene(desktop, control);

            Assert.DoesNotThrow(() =>
            {
                _sceneMethods.RemoveControlFromScene(desktop, control);
            });
        }

        [Test]
        public void RemoveControlFromScene_TwoControls_OneShouldRemain()
        {
            var desktop = _sceneMethods.CreateScene();
            Control control1 = new Control();
            Control control2 = new Control();
            _sceneMethods.AddControlToScene(desktop, control1);
            _sceneMethods.AddControlToScene(desktop, control2);

            _sceneMethods.RemoveControlFromScene(desktop, control1);

            bool hasControl1 = desktop.Controls.Contains(control1);
            bool hasControl2 = desktop.Controls.Contains(control2);

            Assert.IsFalse(hasControl1);
            Assert.IsTrue(hasControl2);

        }

        [Test]
        public void ChangeScene_DoesNotThrow()
        {
            var desktop = _sceneMethods.CreateScene();

            Assert.DoesNotThrow(() =>
            {
                _sceneMethods.ChangeScene(desktop);
            });
        }

    }
}
