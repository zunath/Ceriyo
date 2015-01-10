using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.DataObjects;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
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

        public EditableMapDrawableBatch(Area area, GameResource graphicResource) 
            : base(area, graphicResource, true)
        {
            SelectedTiles = new List<Vector3Int>();
            MouseLayer = 0;
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
            MouseTileX = InputManager.Mouse.X / EngineConstants.TilePixelWidth;
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
                    Layer layer = AreaMap.Layers[location.Z];
                    Tile tile = layer.Tiles[location.X, location.Y];

                    // We temporarily create a new tile using the system tilesheet so that we
                    // can specify a color for highlighting. We need to remove it here to do cleanup.
                    if (tile.TileSheet == _systemTileSheet && location.Z > 0)
                    {
                        layer.Tiles[location.X, location.Y] = null;
                    }
                    else
                    {
                        layer.Tiles[location.X, location.Y].TileColor = Color.White;
                    }
                }

                SelectedTiles.Clear();
            }
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

                    if (x >= 0 && x <= xEnd &&
                        y >= 0 && y <= yEnd)
                    {
                        SelectedTiles.Add(new Vector3Int(x, y, MouseLayer));
                    }
                }
            }

            foreach (Vector3Int coords in SelectedTiles)
            {
                Layer layer = AreaMap.Layers[coords.Z];
                Tile tile = layer.Tiles[coords.X, coords.Y];

                if (tile == null)
                {
                    layer.Tiles[coords.X, coords.Y] = new StaticTile(layer, _systemTileSheet, BlendMode.Alpha, 1);
                }

                layer.Tiles[coords.X, coords.Y].TileColor = new Color(255, 63, 73, 125);
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
            else if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.RightButton))
            {
                Layer layer = AreaMap.Layers[MouseLayer];
                Tile tile = layer.Tiles[MouseTileX, MouseTileY];

                if (tile == null) return;
                if (MouseLayer == 0)
                {
                    layer.Tiles[MouseTileX, MouseTileY] = new StaticTile(layer, _systemTileSheet, BlendMode.Alpha, 0);
                }
                else
                {
                    layer.Tiles[MouseTileX, MouseTileY] = null;
                    SelectedTiles.RemoveAll(t => t.X == MouseTileX && t.Y == MouseTileY && t.Z == MouseLayer);
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

        public void SetActiveLayer(int layerID)
        {
            if (layerID < 0) layerID = 0;
            else if (layerID > EngineConstants.AreaMaxLayers) layerID = EngineConstants.AreaMaxLayers;

            MouseLayer = layerID;

            foreach (Layer layer in AreaMap.Layers)
            {
                layer.ColorOverride = Color.White;
            }

            for (int layer = 0; layer < AreaMap.Layers.Count; layer++)
            {
                if (layer == MouseLayer) continue;

                AreaMap.Layers[layer].ColorOverride = Color.LightGray;
            }
        }
    }
}
