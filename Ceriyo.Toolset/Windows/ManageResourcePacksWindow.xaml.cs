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
        }

        private void Initialize()
        {
            Model = new ManageResourcePacksVM();
        }

        private void SetDataContexts()
        {
            ddlAvailableResourcePackages.DataContext = Model;
            lbAttachedPackages.DataContext = Model;
        }

        private void MoveUp(object sender, RoutedEventArgs e)
        {
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
        }

        private void AddPackage(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
        }

        private void Save(object sender, RoutedEventArgs e)
        {
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
