using Artemis;
using Ceriyo.Core.Systems.Draw;
using Ceriyo.Core.Systems.Update;
using Ceriyo.Testing.Shared;
using Ceriyo.Toolset.WPF.GameWorld;
using NUnit.Framework;

namespace Ceriyo.Toolset.WPF.Tests.GameWorld
{
    public class ToolsetSystemLoaderTests
    {
        private EntityWorld _world;
        private ToolsetSystemLoader _loader;

        private AreaRenderSystem _areaRenderSystem;
        private RenderSystem _renderSystem;
        private PainterSystem _painterSystem;


        [SetUp]
        public void SetUp()
        {
            _world = TestHelpers.CreateEntityWorld();
            _areaRenderSystem = new AreaRenderSystem(null, null, null);
            _renderSystem = new RenderSystem(null);
            _painterSystem = new PainterSystem(null, null, null);
            _loader = new ToolsetSystemLoader(
                _world,
                _areaRenderSystem,
                _renderSystem,
                _painterSystem);

            _loader.LoadSystems();
        }

        [Test]
        public void ToolsetSystemLoader_LoadSystems_SystemsShouldNotBeNull()
        {
            Assert.IsNotNull(_world.SystemManager.GetSystem<AreaRenderSystem>());
            Assert.IsNotNull(_world.SystemManager.GetSystem<RenderSystem>());
            Assert.IsNotNull(_world.SystemManager.GetSystem<PainterSystem>());
        }

        [Test]
        public void ToolsetSystemLoader_LoadSystems_ShouldNotHaveExtraSystemsLoaded()
        {
            var count = _world.SystemManager.Systems.Count;

            Assert.AreEqual(3, count);
        }

    }
}
