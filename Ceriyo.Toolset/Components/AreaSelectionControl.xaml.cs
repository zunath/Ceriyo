using System;
using System.Collections.Generic;
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

        public AreaSelectionControl()
        {
            InitializeComponent();
            this.Model = new AreaSelectionVM();
            this.Loaded += LoadWindow;

            SetDataContexts();
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            lbAreas.DataContext = Model;
        }

        public void Populate()
        {
            Model.Areas.Clear();
            string[] areaFiles = Directory.GetFiles(WorkingPaths.AreasDirectory);
            foreach (string area in areaFiles)
            {
                Model.Areas.Add(FileManager.XmlDeserialize<Area>(area));
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Area area = lbAreas.SelectedItem as Area;

            if (OnAreaOpen != null)
            {
                OnAreaOpen(this, new GameObjectEventArgs(area));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
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

        private void lbAreas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbAreas.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
