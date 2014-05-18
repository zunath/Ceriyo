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
using Ceriyo.Data.ViewModels;
using Ceriyo.Entities.Screens;
using Ceriyo.Toolset.FRBControl;
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for PaintObjectsControl.xaml
    /// </summary>
    public partial class PaintObjectsControl : UserControl
    {
        private FRBGameComponent TilePickerGame { get; set; }
        private PaintObjectsVM Model { get; set; }
        public event EventHandler<EventArgs> OnTilePicked;
        public event EventHandler<EventArgs> OnModeChanged;

        public PaintObjectsControl()
        {
            InitializeComponent();
            Model = new PaintObjectsVM();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            rectSelectedTiles.DataContext = Model;
        }

        private void PopulateModel()
        {
            Model.Creatures = WorkingDataManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
            Model.Items = WorkingDataManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.Placeables = WorkingDataManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
            Model.PaintMode = PaintObjectModeTypeEnum.None;
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
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point pos = e.GetPosition(cnvTilePicker);
                int cellX = (int)pos.X / EngineConstants.TilePixelWidth;
                int cellY = (int)pos.Y / EngineConstants.TilePixelHeight;

                Canvas.SetLeft(rectSelectedTiles, cellX * EngineConstants.TilePixelWidth);
                Canvas.SetTop(rectSelectedTiles, cellY * EngineConstants.TilePixelHeight);

            }
        }

    }
}
