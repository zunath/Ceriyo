using System;
using System.Windows;
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
            DataContext = Model;
        }

        private void Initialize()
        {
            Model.AvailableResourcePackages = ResourcePackDataManager.GetAllResourcePackNames();
            Model.AttachedResourcePackages = WorkingDataManager.GetGameModule().ResourcePacks;
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
            ResourcePackDataManager.BuildModule(Model.AttachedResourcePackages);
            Model.AttachedResourcePackages.Clear();
            Model.AvailableResourcePackages.Clear();
            Hide();
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
