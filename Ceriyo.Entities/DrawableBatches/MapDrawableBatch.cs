using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using Microsoft.Xna.Framework.Graphics;
using xTile;
using xTile.Dimensions;
using xTile.Display;
using xTile.Tiles;


namespace Ceriyo.Entities.DrawableBatches
{
    public class MapDrawableBatch: PositionedObject, IDrawableBatch
    {
        protected Area DrawableArea { get; set; }
        protected Map AreaMap { get; set; }
        private readonly IDisplayDevice _displayDevice;
        protected Rectangle _viewport;
        private TileSheet _areaTileSheet;
        protected TileSheet _systemTileSheet;
        private readonly Location _offset;

        public MapDrawableBatch(Area area)
        {
            DrawableArea = area;
            _displayDevice = new XnaDisplayDevice(FlatRedBallServices.GetContentManagerByName(FlatRedBallServices.GlobalContentManager),
                FlatRedBallServices.GraphicsDevice);
            _viewport = new Rectangle(FlatRedBallServices.GraphicsDevice.Viewport.X,
                FlatRedBallServices.GraphicsDevice.Viewport.Y,
                FlatRedBallServices.GraphicsDevice.Viewport.Width,
                FlatRedBallServices.GraphicsDevice.Viewport.Height);

            FlatRedBallServices.CornerGrabbingResize += FlatRedBallServices_CornerGrabbingResize;

            AreaMap = new Map(DrawableArea.Resref);
            _offset = new Location(0, 0);

            LoadTileSheets();
            LoadLayers();

            SpriteManager.AddDrawableBatch(this);
            SpriteManager.AddPositionedObject(this);
        }

        private void FlatRedBallServices_CornerGrabbingResize(object sender, System.EventArgs e)
        {
            _viewport = new Rectangle(FlatRedBallServices.GraphicsDevice.Viewport.X,
                FlatRedBallServices.GraphicsDevice.Viewport.Y,
                FlatRedBallServices.GraphicsDevice.Viewport.Width,
                FlatRedBallServices.GraphicsDevice.Viewport.Height);
        }

        private void LoadTileSheets()
        {
            Texture2D texture = GameResourceProcessor.ToTexture2D(DrawableArea.AreaTileset.Graphic);

            _areaTileSheet = new TileSheet(AreaMap,
                DrawableArea.AreaTileset.Graphic,
                new Size(texture.Width, texture.Height),  
                new Size(EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight));

            _systemTileSheet = new TileSheet(AreaMap,
                "systemtiles.png",
                new Size(EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight),
                new Size(EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight));

            _displayDevice.LoadTileSheet(_areaTileSheet);
            _displayDevice.LoadTileSheet(_systemTileSheet);

            AreaMap.AddTileSheet(_areaTileSheet);
            AreaMap.AddTileSheet(_systemTileSheet);
        }

        private void LoadLayers()
        {
            for (int layer = 0; layer < DrawableArea.LayerCount; layer++)
            {
                xTile.Layers.Layer areaLayer = new xTile.Layers.Layer(string.Format("l{0}_{1}", layer, DrawableArea.Resref),
                    AreaMap,
                    new Size(DrawableArea.MapWidth, DrawableArea.MapHeight),
                    new Size(EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight));

                List<MapTile> tiles = DrawableArea.MapTiles.Where(t => t.Layer == layer).ToList();
                foreach (MapTile tile in tiles)
                {
                    if (!tile.HasGraphic && layer == 0)
                    {
                        areaLayer.Tiles[tile.MapX, tile.MapY] = new StaticTile(areaLayer, _systemTileSheet, BlendMode.Alpha, 0);    
                    }
                    else if(tile.HasGraphic)
                    {
                        int tileIndex = _areaTileSheet.GetTileIndex(new Location(tile.MapX, tile.MapY));
                        areaLayer.Tiles[tile.MapX, tile.MapY] = new StaticTile(areaLayer, _areaTileSheet, BlendMode.Alpha, tileIndex);
                    }
                    
                }

                AreaMap.AddLayer(areaLayer);
            }

        }

        public virtual void Destroy()
        {
            AreaMap.DisposeTileSheets(_displayDevice);
            AreaMap = null;
        }

        public virtual void Draw(Camera camera)
        {
            if (AreaMap != null)
            {
                AreaMap.Draw(_displayDevice, _viewport, _offset, false);    
            }
        }

        public virtual void Update()
        {
            if (AreaMap != null)
            {
                AreaMap.Update(TimeManager.LastUpdateGameTime.ElapsedGameTime.Milliseconds);    
            }
        }

        public bool UpdateEveryFrame
        {
            get { return true; }
        }

        public void AreaPropertiesSaved(object sender, AreaPropertiesChangedEventArgs e)
        {
            // Area modified is currently open and its dimensions have changed.
            if(e.ModifiedArea.Resref == DrawableArea.Resref && 
               (e.ModifiedArea.MapWidth != DrawableArea.MapWidth ||
               e.ModifiedArea.MapHeight != DrawableArea.MapHeight))
            {

            }
        }

    }
}
