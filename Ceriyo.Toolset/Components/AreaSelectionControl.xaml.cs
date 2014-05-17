using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Toolset.Windows;
using FlatRedBall.IO;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AreaSelectionControl.xaml
    /// </summary>
    public partial class AreaSelectionControl : UserControl
    {
        protected AreaSelectionVM Model { get; set; }
        public event EventHandler<GameObjectEventArgs> OnAreaOpen;
        public event EventHandler<GameObjectEventArgs> OnAreaSaved;
        public event EventHandler<EventArgs> OnAreaClosed;
        private EditAreaWindow EditPropertiesWindow { get; set; }

        public AreaSelectionControl()
        {
            InitializeComponent();
            InitializeModel();    
            SetDataContexts();
            EditPropertiesWindow = new EditAreaWindow();
            EditPropertiesWindow.OnSaveArea += OnSaveArea;
        }

        private void InitializeModel()
        {
            this.Model = new AreaSelectionVM();
            Model.Areas = WorkingDataManager.GetAllGameObjects<Area>(ModulePaths.AreasDirectory) as BindingList<Area>;
        }

        private void SetDataContexts()
        {
            lbAreas.DataContext = Model;
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            Area area = new Area();
            EditPropertiesWindow.Open(area, false);
        }

        private void OnSaveArea(object sender, GameObjectEventArgs e)
        {
            Area area = Model.Areas.SingleOrDefault(x => x.Resref == e.GameObject.Resref);
            
            if (area == null)
            {
                Model.Areas.Add(e.GameObject as Area);
            }
            else
            {
                int index = Model.Areas.IndexOf(area);
                Model.Areas[index] = e.GameObject as Area;
            }

            if(OnAreaSaved != null)
            {
                OnAreaSaved(this, e);
            }

        }

        private void Open(object sender, RoutedEventArgs e)
        {
            Area area = lbAreas.SelectedItem as Area;

            if (area != null)
            {
                if (OnAreaOpen != null)
                {
                    OnAreaOpen(this, new GameObjectEventArgs(area));
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Area area = lbAreas.SelectedItem as Area;
            if (area != null)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete the area " + area.Name + " ?", "Delete Area?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        FileOperationResultTypeEnum result = WorkingDataManager.DeleteGameObjectFile(area);

                        if (result == FileOperationResultTypeEnum.Success)
                        {
                            Model.Areas.Remove(area);

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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void DoubleClickItem(object sender, MouseButtonEventArgs e)
        {
            if (lbAreas.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuNew(object sender, RoutedEventArgs e)
        {
            if (lbAreas.SelectedItem != null)
            {
                btnCreate.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuOpen(object sender, RoutedEventArgs e)
        {
            if (lbAreas.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
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
    }
}
