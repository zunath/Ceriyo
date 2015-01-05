using System;
using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework.Graphics;
using xTile;
using xTile.Dimensions;
using xTile.Display;
using xTile.Tiles;


namespace Ceriyo.Entities.DrawableBatches
{
    public class MapDrawableBatch: PositionedObject, IDrawableBatch
    {
        protected Area DrawableArea { get; private set; }
        protected Map AreaMap { get; private set; }
        private readonly IDisplayDevice _displayDevice;
        private Rectangle _viewport;
        protected TileSheet _areaTileSheet;
        protected TileSheet _systemTileSheet;
        private readonly Location _offset;
        private readonly GameResource _graphicResource;
        private bool DisplayGridLines { get; set; }

        public MapDrawableBatch(Area area, GameResource graphicResource, bool displayGridLines = false)
        {
            DrawableArea = area;
            _graphicResource = graphicResource;
            DisplayGridLines = displayGridLines;
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
        }

        private void FlatRedBallServices_CornerGrabbingResize(object sender, EventArgs e)
        {
            _viewport = new Rectangle(FlatRedBallServices.GraphicsDevice.Viewport.X,
                FlatRedBallServices.GraphicsDevice.Viewport.Y,
                FlatRedBallServices.GraphicsDevice.Viewport.Width,
                FlatRedBallServices.GraphicsDevice.Viewport.Height);
        }

        private void LoadTileSheets()
        {
            Texture2D texture = GameResourceProcessor.ToTexture2D(_graphicResource);

            _areaTileSheet = new TileSheet(AreaMap,
                _graphicResource,
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
                    if (!tile.HasGraphic && layer == 0 && DisplayGridLines)
                    {
                        areaLayer.Tiles[tile.MapX, tile.MapY] = new StaticTile(areaLayer, _systemTileSheet, BlendMode.Alpha, 0);    
                    }
                    else if(tile.HasGraphic)
                    {
                        int tileIndex = _areaTileSheet.GetTileIndex(
                            new Location(tile.TileSheetX * EngineConstants.TilePixelWidth, 
                                         tile.TileSheetY * EngineConstants.TilePixelHeight));
                        areaLayer.Tiles[tile.MapX, tile.MapY] = new StaticTile(areaLayer, _areaTileSheet, BlendMode.Alpha, tileIndex);
                    }
                    
                }

                AreaMap.AddLayer(areaLayer);
            }

        }

        public virtual void Destroy()
        {
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

        public List<MapTile> GetMapTiles()
        {
            List<MapTile> mapTiles = new List<MapTile>();

            for (int layer = 0; layer < AreaMap.Layers.Count; layer++)
            {
                int layerWidth = AreaMap.Layers[layer].LayerWidth;
                int layerHeight = AreaMap.Layers[layer].LayerHeight;

                for (int x = 0; x < layerWidth; x++)
                {
                    for (int y = 0; y < layerHeight; y++)
                    {
                        Tile tile = AreaMap.Layers[layer].Tiles[x, y];
                        int tileSheetX = 0;
                        int tileSheetY = 0;
                        bool hasGraphic = false;

                        if (tile != null)
                        {
                            var dimensions = tile.TileSheet.GetTileImageBounds(tile.TileIndex);
                            tileSheetX = dimensions.X / EngineConstants.TilePixelWidth;
                            tileSheetY = dimensions.Y / EngineConstants.TilePixelHeight;
                            hasGraphic = tile.TileSheet != _systemTileSheet;
                        }

                        mapTiles.Add(new MapTile
                        {
                            HasGraphic = hasGraphic,
                            MapX = x,
                            MapY = y,
                            TileSheetX = tileSheetX,
                            TileSheetY = tileSheetY,
                            Layer = layer
                        });

                    }
                }

            }

            return mapTiles;
        }
    }
}
