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

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for TilesetEditorControl.xaml
    /// </summary>
    public partial class TilesetEditorControl : UserControl
    {
        private TilesetEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }

        public TilesetEditorControl()
        {
            InitializeComponent();
            Model = new TilesetEditorVM();
            Processor = new GameResourceProcessor();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            lbTilesets.DataContext = Model;
            lbGraphics.DataContext = Model;
            txtName.DataContext = Model;
            txtTag.DataContext = Model;
            txtResref.DataContext = Model;
            btnNew.DataContext = Model;
            btnDelete.DataContext = Model;
            radPassage.DataContext = Model;
            radPassage4Direction.DataContext = Model;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Tileset tileset = new Tileset();
            string resref = Processor.GenerateUniqueResref(tileset);

            tileset.Name = resref;
            tileset.Tag = resref;
            tileset.Resref = resref;

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
                    Model.IsTilesetSelected = false;
                }
            }
        }

        public void Save(object sender, EventArgs e)
        {
            foreach (Tileset tileset in Model.Tilesets)
            {
                FileOperationResultTypeEnum result = WorkingDataManager.SaveGameObjectFile(tileset);

                if (result != FileOperationResultTypeEnum.Success)
                {
                    MessageBox.Show("Unable to save tileset: '" + tileset.Name + "'", "Saving tileset failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackDataManager.GetGameResources(ResourceTypeEnum.Graphic);
            Model.Tilesets = WorkingDataManager.GetAllGameObjects<Tileset>(ModulePaths.TilesetsDirectory);

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
                BitmapImage image = Processor.ToBitmapImage(resource);
                Model.SelectedTileset.Graphic = resource;
                imgGraphic.Source = image;   
            }
        }


    }
}
