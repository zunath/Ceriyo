using System;
using System.Collections.Generic;
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
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for PlaceableEditorControl.xaml
    /// </summary>
    public partial class PlaceableEditorControl : UserControl
    {
        private PlaceableEditorVM Model { get; set; }

        public PlaceableEditorControl()
        {
            InitializeComponent();
            Model = new PlaceableEditorVM();
            DataContext = Model;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Placeable placeable = new Placeable();
            string resref = GameResourceProcessor.GenerateUniqueResref(Model.Placeables.Cast<IGameObject>().ToList(), placeable.CategoryName);

            placeable.Name = resref;
            placeable.Tag = resref;
            placeable.Resref = resref;
            Model.Placeables.Add(placeable);
            int index = lbPlaceables.Items.IndexOf(placeable);
            lbPlaceables.SelectedItem = lbPlaceables.Items[index];
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Placeable placeable = lbPlaceables.SelectedItem as Placeable;

            if (placeable != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this placeable?", "Delete Placeable?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Placeables.Remove(placeable);
                    Model.SelectedPlaceable = null;
                    Model.IsPlaceableSelected = false;
                    // TODO: set images source to null
                }
            }
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultType result = WorkingDataManager.ReplaceAllGameObjectFiles(Model.Placeables.Cast<IGameObject>().ToList(), WorkingPaths.PlaceablesDirectory);

            if (result != FileOperationResultType.Success)
            {
                MessageBox.Show("Unable to save placeables.", "Saving placeables failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackDataManager.GetGameResources(ResourceType.Graphic, ResourceSubType.Placeable);
            GameResource graphic = new GameResource("", "(No Graphic)", ResourceType.None, ResourceSubType.None);
            Model.Graphics.Insert(0, graphic);

            Model.Placeables = WorkingDataManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
            Model.Scripts = WorkingDataManager.GetAllScriptNames();

            foreach (Placeable placeable in Model.Placeables)
            {
                GameResource resource = Model.Graphics.SingleOrDefault(x => x.FileName == placeable.Graphic.FileName);
                if (resource != null)
                {
                    placeable.Graphic = resource;
                }
            }

            if (Model.Placeables.Count > 0)
            {
                lbPlaceables.SelectedItem = Model.Placeables[0];
            }
        }

        private void PlaceableSelected(object sender, SelectionChangedEventArgs e)
        {
            Placeable placeable = lbPlaceables.SelectedItem as Placeable;
            Model.SelectedPlaceable = placeable;
            Model.IsPlaceableSelected = placeable == null ? false : true;
        }

    }
}
