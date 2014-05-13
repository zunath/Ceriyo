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
            Initialize();
        }

        private void Initialize()
        {
            Model = new PaintObjectsVM();
            PopulateModel();
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
        }

        public void LoadModule(object sender, GameModuleEventArgs e)
        {
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            Area area = e.GameObject as Area;
            GameResourceProcessor processor = new GameResourceProcessor();
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(processor.ToBytes(area.Graphic));
            image.EndInit();

            imgTiles.Source = image;
        }

    }
}
