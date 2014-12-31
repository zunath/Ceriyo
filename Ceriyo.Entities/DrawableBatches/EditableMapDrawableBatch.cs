using System.Linq;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using FlatRedBall.Input;
using Microsoft.Xna.Framework;
using xTile.Dimensions;
using xTile.Tiles;

namespace Ceriyo.Entities.DrawableBatches
{
    public class EditableMapDrawableBatch : MapDrawableBatch
    {
        private int CurrentLayer { get; set; }
        private int SelectedTileX { get; set; }
        private int SelectedTileY { get; set; }
        private int SelectionTileWidth { get; set; }
        private int SelectionTileHeight { get; set; }

        private int LastFrameX { get; set; }
        private int LastFrameY { get; set; }
        private int LastFrameLayer { get; set; }
        private Tile LastFrameTile { get; set; }

        public EditableMapDrawableBatch(Area area) 
            : base(area)
        {
        }

        public override void Update()
        {
            HighlightSelectedTile();
            PaintTiles();

            base.Update();
        }

        private void HighlightSelectedTile()
        {
            if (LastFrameTile != null)
            {
                AreaMap.Layers[LastFrameLayer].Tiles[LastFrameX, LastFrameY].TileColor = Color.White;
            }

            int x = InputManager.Mouse.X / EngineConstants.TilePixelWidth;
            int y = InputManager.Mouse.Y / EngineConstants.TilePixelHeight;
            int layer = 0;

            MapTile mapTile = DrawableArea.MapTiles
                    .OrderByDescending(l => l.Layer)
                    .FirstOrDefault(t => t.HasGraphic &&
                                            t.MapX == x &&
                                            t.MapY == y);
            if (mapTile != null)
            {
                layer = mapTile.Layer;
            }
            
            Location selectedLocation = new Location(x, y);
            LastFrameTile = AreaMap.Layers[layer].PickTile(selectedLocation, _viewport.Size);

            if (LastFrameTile != null)
            {
                AreaMap.Layers[layer].Tiles[x, y].TileColor = new Color(255, 63, 73, 125);
            }

            LastFrameX = x;
            LastFrameY = y;
            LastFrameLayer = layer;
        }

        private void PaintTiles()
        {
            if (InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton))
            {
                
            }
        }

        public void TilesSelected(object sender, TilePaintEventArgs e)
        {
            
        }
    }
}
