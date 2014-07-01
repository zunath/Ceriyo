﻿using System;
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
using Ceriyo.Data.ViewModels;
using Ceriyo.Entities.Screens;
using Ceriyo.Toolset.FRBControl;
using Ceriyo.Library.Processing;
using System.ComponentModel;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for PaintObjectsControl.xaml
    /// </summary>
    public partial class PaintObjectsControl : UserControl
    {
        private PaintObjectsVM Model { get; set; }
        public event EventHandler<ObjectPainterEventArgs> OnModeChange;
        private WorkingDataManager WorkingManager { get; set; }

        public PaintObjectsControl()
        {
            InitializeComponent();
            Model = new PaintObjectsVM();
            WorkingManager = new WorkingDataManager();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            rectSelectedTiles.DataContext = Model;
            lbCreatures.DataContext = Model;
            lbItems.DataContext = Model;
            lbPlaceables.DataContext = Model;
            ObjectTabs.DataContext = Model;
        }

        private void PopulateModel()
        {
            Model.Creatures = WorkingManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
            Model.Items = WorkingManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.Placeables = WorkingManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
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
        }

        public void GameObjectsListsChanged(object sender, EventArgs e)
        {
            Model.Items = WorkingManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.Placeables = WorkingManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
            Model.Creatures = WorkingManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            Area area = e.GameObject as Area;

            if (area.AreaTileset != null)
            {
                if (!string.IsNullOrWhiteSpace(area.AreaTileset.Graphic.FileName))
                {
                    GameResourceProcessor processor = new GameResourceProcessor();
                    imgTiles.Source = processor.ToBitmapImage(area.AreaTileset.Graphic);
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

        public void ModulePropertiesUpdated(object sender, GameObjectEventArgs e)
        {
            UnloadArea(sender, new EventArgs());
            LoadArea(sender, e);
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
