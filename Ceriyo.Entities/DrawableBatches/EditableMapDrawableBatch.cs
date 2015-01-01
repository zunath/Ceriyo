using System.Linq;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using FlatRedBall;
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

        private int LastFrameX { get; set; }
        private int LastFrameY { get; set; }
        private int LastFrameLayer { get; set; }
        private Tile LastFrameTile { get; set; }

        private int MouseLayer { get; set; }
        private int MouseTileX { get; set; }
        private int MouseTileY { get; set; }

        public EditableMapDrawableBatch(Area area) 
            : base(area)
        {
        }

        public override void Update()
        {
            if (InputManager.Mouse.IsInGameWindow())
            {
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

        private void HighlightSelectedTile()
        {
            if (LastFrameTile != null)
            {
                AreaMap.Layers[LastFrameLayer].Tiles[LastFrameX, LastFrameY].TileColor = Color.White;
            }
            int layer = 0;
            MapTile mapTile = DrawableArea.MapTiles
                    .OrderByDescending(l => l.Layer)
                    .FirstOrDefault(t => t.HasGraphic &&
                                            t.MapX == MouseTileX &&
                                            t.MapY == MouseTileY);
            if (mapTile != null)
            {
                layer = mapTile.Layer;
            }
            
            Location selectedLocation = new Location(MouseTileX, MouseTileY);
            LastFrameTile = AreaMap.Layers[layer].PickTile(selectedLocation, _viewport.Size);

            if (LastFrameTile != null)
            {
                AreaMap.Layers[layer].Tiles[MouseTileX, MouseTileY].TileColor = new Color(255, 63, 73, 125);
            }

            LastFrameX = MouseTileX;
            LastFrameY = MouseTileY;
            LastFrameLayer = layer;
        }

        private void PaintTiles()
        {
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
            {
                Layer layer = AreaMap.Layers[MouseLayer];
                int tileIndex = _areaTileSheet.GetTileIndex(new Location(TileSheetXStart * EngineConstants.TilePixelWidth, 
                    TileSheetYStart * EngineConstants.TilePixelHeight));

                layer.Tiles[MouseTileX, MouseTileY] = new StaticTile(layer, _areaTileSheet, BlendMode.Alpha, tileIndex);

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
