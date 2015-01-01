using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.DataObjects;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using FlatRedBall.Input;
using Microsoft.Xna.Framework;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace Ceriyo.Entities.DrawableBatches
{
    public class EditableMapDrawableBatch : MapDrawableBatch
    {
        private int TileSheetXStart { get; set; }
        private int TileSheetYStart { get; set; }
        private int TileSheetXEnd { get; set; }
        private int TileSheetYEnd { get; set; }

        private List<Vector3Int> SelectedTiles { get; set; }

        private int MouseLayer { get; set; }
        private int MouseTileX { get; set; }
        private int MouseTileY { get; set; }

        public EditableMapDrawableBatch(Area area) 
            : base(area)
        {
            SelectedTiles = new List<Vector3Int>();
        }
        
        public override void Update()
        {
            if (InputManager.Mouse.IsInGameWindow())
            {
                ResetTileHighlighting();
                UpdateTileSelected();
                HighlightSelectedTile();
                PaintTiles();
            }
            base.Update();
        }

        private void UpdateTileSelected()
        {
            MouseTileX = InputManager.Mouse.X/EngineConstants.TilePixelWidth;
            MouseTileY = InputManager.Mouse.Y / EngineConstants.TilePixelHeight;
            int xEnd = DrawableArea.MapTiles.Max(t => t.MapX);
            int yEnd = DrawableArea.MapTiles.Max(t => t.MapY);

            if (MouseTileX < 0) MouseTileX = 0;
            else if (MouseTileX > xEnd) MouseTileX = xEnd;

            if (MouseTileY < 0) MouseTileY = 0;
            else if (MouseTileY > yEnd) MouseTileY = yEnd;

        }

        private void ResetTileHighlighting()
        {
            if (SelectedTiles.Count > 0)
            {
                foreach (Vector3Int location in SelectedTiles)
                {
                    AreaMap.Layers[location.Z].Tiles[location.X, location.Y].TileColor = Color.White;
                }
            }
            SelectedTiles.Clear();
        }

        private void HighlightSelectedTile()
        {
            int xEnd = DrawableArea.MapTiles.Max(t => t.MapX);
            int yEnd = DrawableArea.MapTiles.Max(t => t.MapY);
            int tilesWide = TileSheetXEnd - TileSheetXStart;
            int tilesHigh = TileSheetYEnd - TileSheetYStart;

            for(int w = 0; w <= tilesWide; w++)
            {
                for (int h = 0; h <= tilesHigh; h++)
                {
                    int x = MouseTileX + w;
                    int y = MouseTileY + h;

                    MapTile mapTile = DrawableArea.MapTiles
                            .OrderByDescending(l => l.Layer)
                            .FirstOrDefault(t => t.HasGraphic &&
                                                    t.MapX == x &&
                                                    t.MapY == y);

                    if (x >= 0 && x <= xEnd &&
                        y >= 0 && y <= yEnd)
                    {
                        SelectedTiles.Add(mapTile == null
                            ? new Vector3Int(x, y, 0)
                            : new Vector3Int(x, y, mapTile.Layer));
                    }
                }
            }

            foreach (Vector3Int coords in SelectedTiles)
            {
                AreaMap.Layers[coords.Z].Tiles[coords.X, coords.Y].TileColor = new Color(255, 63, 73, 125);
            }
        }

        private void PaintTiles()
        {
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
            {
                Layer layer = AreaMap.Layers[MouseLayer];
                foreach (Vector3Int coords in SelectedTiles)
                {
                    int selectionWidth = coords.X - MouseTileX;
                    int selectionHeight = coords.Y - MouseTileY;

                    int tileIndex = _areaTileSheet.GetTileIndex(new Location((TileSheetXStart + selectionWidth ) * EngineConstants.TilePixelWidth,
                        (TileSheetYStart + selectionHeight) * EngineConstants.TilePixelHeight));

                    layer.Tiles[coords.X, coords.Y] = new StaticTile(layer, _areaTileSheet, BlendMode.Alpha, tileIndex);
                }
            }
        }

        public void ChangeObjectSelectionMode(object sender, ObjectPainterEventArgs e)
        {
            TileSheetXStart = e.TileCellXStart;
            TileSheetYStart = e.TileCellYStart;
            TileSheetXEnd = e.TileCellXEnd;
            TileSheetYEnd = e.TileCellYEnd;
        }
    }
}
