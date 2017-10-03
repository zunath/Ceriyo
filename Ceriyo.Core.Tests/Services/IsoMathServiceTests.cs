using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Services
{
    public class IsoMathServiceTests
    {
        private IIsoMathService _isoMathService;

        [SetUp]
        public void IsoMathServiceSetup()
        {
            _isoMathService = new IsoMathService(new EngineService());
        }

        [Test]
        public void IsoMathService_MapTileToScreenPositionINT_ShouldEqualX32Y48()
        {
            var result = _isoMathService.MapTileToScreenPosition(2, 1);

            Assert.AreEqual(32, result.X);
            Assert.AreEqual(48, result.Y);
        }

        [Test]
        public void IsoMathService_MapTileToScreenPositionFLOAT_ShouldEqualX32Y48()
        {
            var result = _isoMathService.MapTileToScreenPosition(2.0f, 1.0f);

            Assert.AreEqual(32, result.X);
            Assert.AreEqual(48, result.Y);
        }

        [Test]
        public void IsoMathService_MapTileToScreenPositionVECTOR2_ShouldEqualX32Y48()
        {
            var result = _isoMathService.MapTileToScreenPosition(new Vector2(2, 1));

            Assert.AreEqual(32, result.X);
            Assert.AreEqual(48, result.Y);
        }
    }
}
