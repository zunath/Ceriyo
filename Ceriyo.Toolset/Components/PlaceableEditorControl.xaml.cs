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
        private GameResourceProcessor Processor { get; set; }
        private ResourcePackDataManager ResourcePackManager { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public PlaceableEditorControl()
        {
            InitializeComponent();
            Model = new PlaceableEditorVM();
            Processor = new GameResourceProcessor();
            ResourcePackManager = new ResourcePackDataManager();
            WorkingManager = new WorkingDataManager();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            txtName.DataContext = Model;
            txtResref.DataContext = Model;
            txtTag.DataContext = Model;
            lbPlaceables.DataContext = Model;
            chkAutoRemoveKey.DataContext = Model;
            chkIsKeyRequired.DataContext = Model;
            chkIsLocked.DataContext = Model;
            chkIsPlot.DataContext = Model;
            chkIsStatic.DataContext = Model;
            chkIsUseable.DataContext = Model;
            txtKeyTag.DataContext = Model;
            txtDescription.DataContext = Model;
            txtComments.DataContext = Model;
            dgLocalVariables.DataContext = Model;
            ddlOnAttackedScript.DataContext = Model;
            ddlOnClosedScript.DataContext = Model;
            ddlOnDamagedScript.DataContext = Model;
            ddlOnDeathScript.DataContext = Model;
            ddlOnDisturbedScript.DataContext = Model;
            ddlOnHeartbeatScript.DataContext = Model;
            ddlOnLocked.DataContext = Model;
            ddlOnOpenScript.DataContext = Model;
            ddlOnUnlockedScript.DataContext = Model;
            ddlOnUsedScript.DataContext = Model;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Placeable placeable = new Placeable();
            string resref = Processor.GenerateUniqueResref(Model.Placeables.Cast<IGameObject>().ToList(), placeable.CategoryName);

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
            foreach (Placeable placeable in Model.Placeables)
            {
                FileOperationResultTypeEnum result = WorkingManager.SaveGameObjectFile(placeable);

                if (result != FileOperationResultTypeEnum.Success)
                {
                    MessageBox.Show("Unable to save placeable: '" + placeable.Name + "'", "Saving placeable failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackManager.GetGameResources(ResourceTypeEnum.Graphic);
            GameResource graphic = new GameResource("", "(No Graphic)", ResourceTypeEnum.None);
            Model.Graphics.Insert(0, graphic);

            Model.Placeables = WorkingManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
            Model.Scripts = WorkingManager.GetAllScriptNames();

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
