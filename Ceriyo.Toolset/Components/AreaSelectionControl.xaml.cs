using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AreaSelectionControl.xaml
    /// </summary>
    public partial class AreaSelectionControl
    {
        private AreaSelectionVM Model { get; set; }
        public event EventHandler<GameObjectEventArgs> OnAreaOpen;
        public event EventHandler<EventArgs> OnAreaSaved;
        public event EventHandler<AreaPropertiesChangedEventArgs> OnAreaPropertiesSaved;
        public event EventHandler<EventArgs> OnAreaClosed;
        private EditAreaWindow EditPropertiesWindow { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public AreaSelectionControl()
        {
            InitializeComponent();
            Model = new AreaSelectionVM();
            WorkingManager = new WorkingDataManager();
            DataContext = Model;
            EditPropertiesWindow = new EditAreaWindow();
            EditPropertiesWindow.OnSaveAreaProperties += SavedAreaProperties;
            
        }

        public void ModuleLoaded(object sender, GameModuleEventArgs e)
        {
            Model.Areas.Clear();
            Model.IsAreaLoaded = false;
            Model.IsModuleLoaded = true;

            if (OnAreaClosed != null)
            {
                OnAreaClosed(this, new EventArgs());
            }

            Model.Areas = WorkingManager.GetAllGameObjects<Area>(ModulePaths.AreasDirectory);

        }

        private void Create(object sender, RoutedEventArgs e)
        {
            Area area = new Area();
            EditPropertiesWindow.Open(area, false);
        }

        private void SavedAreaProperties(object sender, AreaPropertiesChangedEventArgs e)
        {
            Area area = Model.Areas.SingleOrDefault(x => x.Resref == e.ModifiedArea.Resref);
            
            if (area == null)
            {
                Model.Areas.Add(e.ModifiedArea);
            }
            else
            {
                int index = Model.Areas.IndexOf(area);
                Model.Areas[index] = e.ModifiedArea;
            }

            Model.SelectedArea = e.ModifiedArea;

            if(OnAreaPropertiesSaved != null)
            {
                OnAreaPropertiesSaved(this, new AreaPropertiesChangedEventArgs(e.ModifiedArea, e.IsUpdate));
            }

        }

        private void Save(object sender, RoutedEventArgs e)
        { 
            // Send signal to screen to save the area.
            if (OnAreaSaved != null)
            {
                OnAreaSaved(this, new EventArgs());
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Area area = lbAreas.SelectedItem as Area;
            if (area == null) return;
            if (MessageBox.Show("Are you sure you want to delete the area " + area.Name + " ?", "Delete Area?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileOperationResultTypeEnum result = WorkingManager.DeleteGameObjectFile(area);

                if (result == FileOperationResultTypeEnum.Success)
                {
                    Model.Areas.Remove(area);
                    Model.IsAreaLoaded = false;

                    if (OnAreaClosed != null)
                    {
                        OnAreaClosed(this, new EventArgs());
                    }
                }
                else if (result == FileOperationResultTypeEnum.FileDoesNotExist)
                {
                    MessageBox.Show("Unable to delete area. File does not exist.", "Unable to delete area", MessageBoxButton.OK);
                }
                else if (result == FileOperationResultTypeEnum.Failure)
                {
                    MessageBox.Show("Unable to delete area. Deletion failed.", "Unable to delete area", MessageBoxButton.OK);
                }

            }
        }

        private void OpenArea()
        {
            if (Model.SelectedArea == null) return;
            Model.IsAreaLoaded = true;

            if (OnAreaOpen != null)
            {
                OnAreaOpen(this, new GameObjectEventArgs(Model.SelectedArea));
            }
        }

        private void DoubleClickItem(object sender, MouseButtonEventArgs e)
        {
            OpenArea();
        }

        private void ContextMenuNew(object sender, RoutedEventArgs e)
        {
            btnCreate.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void ContextMenuOpen(object sender, RoutedEventArgs e)
        {
            OpenArea();
        }

        private void ContextMenuEdit(object sender, RoutedEventArgs e)
        {
            Area area = lbAreas.SelectedItem as Area;

            if (lbAreas.SelectedItem != null)
            {
                EditPropertiesWindow.Open(area, true);
            }
        }

        private void ContextMenuDelete(object sender, RoutedEventArgs e)
        {
            if (lbAreas.SelectedItem != null)
            {
                btnDelete.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void AreaSelected(object sender, SelectionChangedEventArgs e)
        {
            Model.IsAreaSelected = Model.SelectedArea != null;
        }
    }
}
