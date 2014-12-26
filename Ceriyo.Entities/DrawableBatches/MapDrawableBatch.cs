using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using xTile;
using xTile.Dimensions;
using xTile.Display;
using xTile.Tiles;


namespace Ceriyo.Entities.DrawableBatches
{
    public class MapDrawableBatch: PositionedObject, IDrawableBatch
    {
        private Area DrawableArea { get; set; }
        private Map AreaMap { get; set; }
        private readonly IDisplayDevice _displayDevice;
        private Rectangle _viewport;
        private TileSheet _areaTileSheet;
        private TileSheet _emptyTileSheet;
        private Location _offset;

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
            _offset = new Location(100, 100); // TODO: Determine the offset

            LoadTileSheets();
            LoadLayers();

            SpriteManager.AddDrawableBatch(this);
            SpriteManager.AddPositionedObject(this);
        }

        void FlatRedBallServices_CornerGrabbingResize(object sender, System.EventArgs e)
        {
            _viewport = new Rectangle(FlatRedBallServices.GraphicsDevice.Viewport.X,
                FlatRedBallServices.GraphicsDevice.Viewport.Y,
                FlatRedBallServices.GraphicsDevice.Viewport.Width,
                FlatRedBallServices.GraphicsDevice.Viewport.Height);
        }

        private void LoadTileSheets()
        {
            _areaTileSheet = new TileSheet(AreaMap,
                DrawableArea.AreaTileset.Graphic,
                new Size(512, 384),  // TODO: Get the width/height without loading the image into memory.
                new Size(EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight));

            _emptyTileSheet = new TileSheet(AreaMap,
                "emptytile.png",
                new Size(EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight),
                new Size(EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight));

            _displayDevice.LoadTileSheet(_areaTileSheet);
            _displayDevice.LoadTileSheet(_emptyTileSheet);

            AreaMap.AddTileSheet(_areaTileSheet);
            AreaMap.AddTileSheet(_emptyTileSheet);
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
                    if (!tile.HasGraphic)
                    {
                        areaLayer.Tiles[tile.MapX, tile.MapY] = new StaticTile(areaLayer, _emptyTileSheet, BlendMode.Alpha, 0);    
                    }
                    else
                    {
                        areaLayer.Tiles[tile.MapX, tile.MapY] = new StaticTile(areaLayer, _areaTileSheet, BlendMode.Alpha, 0); // TODO: Get correct index
                    }
                    
                }

                AreaMap.AddLayer(areaLayer);
            }

        }

        public void Destroy()
        {
            AreaMap.DisposeTileSheets(_displayDevice);
            AreaMap = null;
        }

        public void Draw(Camera camera)
        {
            if (AreaMap != null)
            {
                AreaMap.Draw(_displayDevice, _viewport, _offset, false);    
            }
        }

        public void Update()
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

        public void PaintTile(object sender, TilePaintEventArgs e)
        {
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
