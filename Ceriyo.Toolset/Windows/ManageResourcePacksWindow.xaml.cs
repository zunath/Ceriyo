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
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ManageResourcePacksWindow.xaml
    /// </summary>
    public partial class ManageResourcePacksWindow : Window
    {
        private ManageResourcePacksVM Model { get; set; }

        public ManageResourcePacksWindow()
        {
            InitializeComponent();
            Model = new ManageResourcePacksVM();
            SetDataContexts();
        }

        private void Initialize()
        {
            Model.AvailableResourcePackages = ResourcePackDataManager.GetAllResourcePackNames();
            Model.AttachedResourcePackages = ModuleDataManager.GetGameModule().ResourcePacks;
        }

        private void SetDataContexts()
        {
            ddlAvailableResourcePackages.DataContext = Model;
            lbAttachedPackages.DataContext = Model;
        }

        private void MoveUp(object sender, RoutedEventArgs e)
        {
            string resourcePack = lbAttachedPackages.SelectedItem as string;

            if (resourcePack != null)
            {
                int oldIndex = Model.AttachedResourcePackages.IndexOf(resourcePack);
                int newIndex = oldIndex - 1;
                if (oldIndex > 0)
                {
                    Model.AttachedResourcePackages.RemoveAt(oldIndex);
                    Model.AttachedResourcePackages.Insert(newIndex, resourcePack);

                    lbAttachedPackages.SelectedIndex = newIndex;
                }
            }
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
            string resourcePack = lbAttachedPackages.SelectedItem as string;

            if (resourcePack != null)
            {
                int oldIndex = Model.AttachedResourcePackages.IndexOf(resourcePack);
                int newIndex = oldIndex + 1;

                if (oldIndex + 1 < Model.AttachedResourcePackages.Count)
                {
                    Model.AttachedResourcePackages.RemoveAt(oldIndex);
                    Model.AttachedResourcePackages.Insert(newIndex, resourcePack);

                    lbAttachedPackages.SelectedIndex = newIndex;
                }
            }

        }

        private void AddPackage(object sender, RoutedEventArgs e)
        {
            string resourcePack = ddlAvailableResourcePackages.SelectedItem as string;

            if (!String.IsNullOrWhiteSpace(resourcePack) &&
                !Model.AttachedResourcePackages.Contains(resourcePack))
            {
                Model.AttachedResourcePackages.Add(resourcePack);
            }
        }

        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
            string resourcePack = lbAttachedPackages.SelectedItem as string;

            if (!String.IsNullOrWhiteSpace(resourcePack))
            {
                Model.AttachedResourcePackages.Remove(resourcePack);
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            bool success = ResourcePackDataManager.BuildModule(Model.AttachedResourcePackages);

            if (success)
            {
                Model.AttachedResourcePackages.Clear();
                Model.AvailableResourcePackages.Clear();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unable to build module.", "Error building module", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Model.AttachedResourcePackages.Clear();
            Model.AvailableResourcePackages.Clear();
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void Open()
        {
            Initialize();
            this.Show();
        }
    }
}
