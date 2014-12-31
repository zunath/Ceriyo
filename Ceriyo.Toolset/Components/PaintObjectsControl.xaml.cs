using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for PaintObjectsControl.xaml
    /// </summary>
    public partial class PaintObjectsControl
    {
        private PaintObjectsVM Model { get; set; }
        public event EventHandler<ObjectPainterEventArgs> OnModeChange;

        public PaintObjectsControl()
        {
            InitializeComponent();
            Model = new PaintObjectsVM();
            DataContext = Model;
        }

        private void PopulateModel()
        {
            Model.Creatures = WorkingDataManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
            Model.Items = WorkingDataManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.Placeables = WorkingDataManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
            Model.PaintMode = PaintObjectModeTypeEnum.Tile;
        }

        private void LoadComponent(object sender, RoutedEventArgs e)
        {
            rectSelectedTiles.Width = EngineConstants.TilePixelWidth;
            rectSelectedTiles.Height = EngineConstants.TilePixelHeight;
        }

        public void UnloadArea(object sender, EventArgs e)
        {
            imgTiles.Source = null;
            Model.Creatures.Clear();
            Model.Items.Clear();
            Model.Placeables.Clear();
            Model.PaintMode = PaintObjectModeTypeEnum.None;
            rectSelectedTiles.Visibility = Visibility.Hidden;
        }

        public void GameObjectsListsChanged(object sender, EventArgs e)
        {
            Model.Items = WorkingDataManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.Placeables = WorkingDataManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
            Model.Creatures = WorkingDataManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            Area area = e.GameObject as Area;

            if (area != null && area.AreaTileset != null)
            {
                if (!string.IsNullOrWhiteSpace(area.AreaTileset.Graphic.FileName))
                {
                    imgTiles.Source = GameResourceProcessor.ToBitmapImage(area.AreaTileset.Graphic);
                }
            }

            PopulateModel();
            Canvas.SetLeft(rectSelectedTiles, 0);
            Canvas.SetTop(rectSelectedTiles, 0);
            rectSelectedTiles.Visibility = Visibility.Visible;
            GameObjectsListsChanged(sender, e);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && rectSelectedTiles.Visibility == Visibility.Visible)
            {
                Model.IsMouseDown = true;
                Point pos = e.GetPosition(cnvTilePicker);
                Model.SelectionStartX = (int)pos.X / EngineConstants.TilePixelWidth;
                Model.SelectionStartY = (int)pos.Y / EngineConstants.TilePixelHeight;

                Canvas.SetLeft(rectSelectedTiles, Model.SelectionStartX * EngineConstants.TilePixelWidth);
                Canvas.SetTop(rectSelectedTiles, Model.SelectionStartY * EngineConstants.TilePixelHeight);

            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && rectSelectedTiles.Visibility == Visibility.Visible)
            {
                Model.IsMouseDown = false;

                if (OnModeChange != null)
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectionStartX, 
                                                                  Model.SelectionStartY, 
                                                                  Model.SelectionEndX, 
                                                                  Model.SelectionEndY));
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Model.IsMouseDown)
            {
                Point pos = e.GetPosition(cnvTilePicker);
                Model.SelectionEndX = (int)pos.X / EngineConstants.TilePixelWidth;
                Model.SelectionEndY = (int)pos.Y / EngineConstants.TilePixelHeight;

                if (Model.SelectionEndX < Model.SelectionStartX)
                {
                    Model.SelectionEndX = Model.SelectionStartX;
                }
                if (Model.SelectionEndY < Model.SelectionStartY)
                {
                    Model.SelectionEndY = Model.SelectionStartY;
                }

                rectSelectedTiles.Width = (Model.SelectionEndX - Model.SelectionStartX + 1) * EngineConstants.TilePixelWidth;
                rectSelectedTiles.Height = (Model.SelectionEndY - Model.SelectionStartY + 1) * EngineConstants.TilePixelHeight;
            }
        }

        public void AreaPropertiesSaved(object sender, AreaPropertiesChangedEventArgs e)
        {
            if (!e.IsUpdate)
            {
                UnloadArea(sender, new EventArgs());
                LoadArea(sender, new GameObjectEventArgs(e.ModifiedArea));
            }
        }

        private void CreatureSelected(object sender, SelectionChangedEventArgs e)
        {
            if (Model.SelectedCreature != null)
            {
                if (OnModeChange != null)
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectedCreature));
                }
            }
        }

        private void ItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (Model.SelectedItem != null)
            {
                if (OnModeChange != null)
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectedItem));
                }
            }
        }

        private void PlaceableSelected(object sender, SelectionChangedEventArgs e)
        {
            if (Model.SelectedPlaceable != null)
            {
                if (OnModeChange != null)
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectedPlaceable));
                }
            }
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem item = ObjectTabs.SelectedItem as TabItem;

            if (item != null && OnModeChange != null)
            {
                if (item.Name == "TilesTab")
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectionStartX,
                                                                    Model.SelectionStartY,
                                                                    Model.SelectionEndX,
                                                                    Model.SelectionEndY));

                }
                else if (item.Name == "CreaturesTab")
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectedCreature));
                }
                else if (item.Name == "ItemsTab")
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectedItem));
                }
                else if (item.Name == "PlaceablesTab")
                {
                    OnModeChange(this, new ObjectPainterEventArgs(Model.SelectedPlaceable));
                }
            }
        }

    }
}
