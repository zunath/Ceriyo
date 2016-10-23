using System;
using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class TileAtlasDataTests
    {
        [Test]
        public void TileAtlasDataTests_SetTile_ShouldAddTile()
        {
            TileAtlasData data = new TileAtlasData();
            var tile = new TileData();
            data.SetTile(3, 5, tile);
            var stored = data.Tiles[new Tuple<int, int>(3, 5)];

            Assert.AreSame(stored, tile);
        }

        [Test]
        public void TileAtlasDataTests_SetTile_ShouldReplaceExistingTile()
        {
            TileAtlasData data = new TileAtlasData();
            var tile = new TileData
            {
                SourceX = 50,
                SourceY = 50
            };

            var tile2 = new TileData
            {
                SourceX = 350,
                SourceY = 350
            };

            data.SetTile(10, 10, tile);
            data.SetTile(10, 10, tile2);

            var stored = data.Tiles[new Tuple<int, int>(10, 10)];
            Assert.AreSame(tile2, stored);
        }

        [Test]
        public void TileAtlasDataTests_GetTile_ShouldReturnNull()
        {
            TileAtlasData data = new TileAtlasData();
            var stored = data.GetTile(10, 10);

            Assert.IsNull(stored);
        }

        [Test]
        public void TileAtlasDataTests_GetTile_ShouldReturnTile()
        {
            TileAtlasData data = new TileAtlasData();
            var tile = new TileData
            {
                SourceX = 30,
                SourceY = 40
            };
            data.SetTile(30, 100, tile);
            var stored = data.GetTile(30, 100);

            Assert.AreSame(stored, tile);
        }

    }
}
