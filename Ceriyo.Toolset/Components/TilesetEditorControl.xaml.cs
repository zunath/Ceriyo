using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;
using Ceriyo.Library.Extensions;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for TilesetEditorControl.xaml
    /// </summary>
    public partial class TilesetEditorControl : UserControl
    {
        private TilesetEditorVM Model { get; set; }
        private ResourcePackDataManager ResourcePackManager { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public TilesetEditorControl()
        {
            InitializeComponent();
            Model = new TilesetEditorVM();
            ResourcePackManager = new ResourcePackDataManager();
            WorkingManager = new WorkingDataManager();
            DataContext = Model;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Tileset tileset = new Tileset();
            string resref = GameResourceProcessor.GenerateUniqueResref(Model.Tilesets.Cast<IGameObject>().ToList(), tileset.CategoryName);

            tileset.Name = resref;
            tileset.Tag = resref;
            tileset.Resref = resref;

            tileset.Graphic = lbGraphics.Items[0] as GameResource;
            Model.Tilesets.Add(tileset);
            int index = lbTilesets.Items.IndexOf(tileset);
            lbTilesets.SelectedItem = lbTilesets.Items[index];
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Tileset tileset = lbTilesets.SelectedItem as Tileset;

            if (tileset != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this tileset?", "Delete Tileset?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Tilesets.Remove(tileset);
                    Model.SelectedTileset = null;
                    Model.IsTilesetSelected = false;
                    imgGraphic.Source = null;
                    LoadPassability();
                }
            }
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Tilesets.Cast<IGameObject>().ToList(), WorkingPaths.TilesetsDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save tilesets", "Saving tilesets failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackManager.GetGameResources(ResourceTypeEnum.Graphic);
            GameResource graphic = new GameResource("", "(No Graphic)", ResourceTypeEnum.None);
            Model.Graphics.Insert(0, graphic);

            Model.Tilesets = WorkingManager.GetAllGameObjects<Tileset>(ModulePaths.TilesetsDirectory);

            // Link graphics to instances in the data context, so that they load on page open.
            foreach (Tileset tileset in Model.Tilesets)
            {
                GameResource resource = Model.Graphics.SingleOrDefault(x => x.FileName == tileset.Graphic.FileName);
                if (resource != null)
                {
                    tileset.Graphic = resource;
                }
            }

            if (Model.Tilesets.Count > 0)
            {
                lbTilesets.SelectedItem = Model.Tilesets[0];
            }
        }

        private void TilesetSelected(object sender, SelectionChangedEventArgs e)
        {
            Tileset tileset = lbTilesets.SelectedItem as Tileset;
            Model.SelectedTileset = tileset;
            Model.IsTilesetSelected = tileset == null ? false : true;

        }

        private void GraphicSelected(object sender, SelectionChangedEventArgs e)
        {
            GameResource resource = lbGraphics.SelectedItem as GameResource;

            if (resource != null)
            {
                if (resource.ResourceType == ResourceTypeEnum.None)
                {
                    imgGraphic.Source = null;
                    ResizeTileList(null);
                    LoadPassability();
                }
                else
                {
                    BitmapImage image = GameResourceProcessor.ToBitmapImage(resource);
                    Model.SelectedTileset.Graphic = resource;
                    imgGraphic.Source = image;

                    ResizeTileList(image);
                    LoadPassability();
                }
            }
        }

        private void ResizeTileList(BitmapImage image)
        {
            if (image == null)
            {
                Model.SelectedTileset.Tiles.Clear();
            }
            else if (Model.SelectedTileset != null)
            {
                // Get how many cells there should be
                int imageCellCountX = image.PixelWidth / EngineConstants.TilePixelWidth;
                int imageCellCountY = image.PixelHeight / EngineConstants.TilePixelHeight;

                // Remove excess cells
                Model.SelectedTileset.Tiles.RemoveAll(x => x.TextureCellX > imageCellCountX ||
                                                           x.TextureCellY > imageCellCountY);

                for (int x = 0; x < imageCellCountX; x++)
                {
                    for (int y = 0; y < imageCellCountY; y++)
                    {
                        if (Model.SelectedTileset.Tiles.SingleOrDefault(tile => tile.TextureCellX == x && tile.TextureCellY == y) == null)
                        {
                            TileDefinition tile = new TileDefinition();
                            tile.TextureCellX = x;
                            tile.TextureCellY = y;
                            Model.SelectedTileset.Tiles.Add(tile);
                        }
                    }
                }
            }
        }

        private void ChangeTileMode(object sender, RoutedEventArgs e)
        {
            if (Model.TileEditMode == TileEditModeEnum.Passability)
            {
                LoadPassability();
            }
            else if(Model.TileEditMode == TileEditModeEnum.Passability4Way)
            {
                LoadPassability4Way();
            }
        }

        private void LoadPassability()
        {
            List<Rectangle> rects = cnvTileEditor.Children.OfType<Rectangle>().ToList();
            
            foreach (Rectangle rect in rects)
            {
                cnvTileEditor.Children.Remove(rect);
            }

            if (Model.SelectedTileset != null)
            {
                foreach (TileDefinition tile in Model.SelectedTileset.Tiles)
                {
                    int x = tile.TextureCellX * EngineConstants.TilePixelWidth;
                    int y = tile.TextureCellY * EngineConstants.TilePixelHeight;

                    Rectangle rect = new Rectangle();
                    rect.Stroke = Brushes.Black;
                    rect.Fill = tile.IsPassable ? Brushes.Green : Brushes.Red;
                    rect.Opacity = 0.2f;
                    rect.Height = EngineConstants.TilePixelHeight;
                    rect.Width = EngineConstants.TilePixelWidth;
                    cnvTileEditor.Children.Add(rect);
                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);
                }
            }
        }

        private void LoadPassability4Way()
        {
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = e.GetPosition(cnvTileEditor);
                Rectangle rect = Mouse.DirectlyOver as Rectangle;

                int cellX = (int)position.X / EngineConstants.TilePixelWidth;
                int cellY = (int)position.Y / EngineConstants.TilePixelHeight;

                if (rect != null)
                {
                    TileDefinition tile = Model.SelectedTileset.Tiles.SingleOrDefault(x => x.TextureCellX == cellX && x.TextureCellY == cellY);

                    if (tile != null)
                    {
                        tile.IsPassable = !tile.IsPassable;
                        rect.Fill = tile.IsPassable ? Brushes.Green : Brushes.Red;
                    }
                }
            }
        }
        
    }
}
