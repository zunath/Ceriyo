using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ceriyo.Data;
using FlatRedBall.IO;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for NewModuleWindow.xaml
    /// </summary>
    public partial class NewModuleWindow : Window
    {
        private NewModuleVM Model { get; set; }

        public NewModuleWindow()
        {
            InitializeComponent();
            Model = new NewModuleVM();
            SetDataContexts();
            SetLimits();
            txtName.Focus();
        }

        private void SetDataContexts()
        {
            txtName.DataContext = Model;
            txtTag.DataContext = Model;
            txtResref.DataContext = Model;
        }

        private void SetLimits()
        {
            txtName.MaxLength = EngineConstants.NameMaxLength;
            txtTag.MaxLength = EngineConstants.TagMaxLength;
            txtResref.MaxLength = EngineConstants.ResrefMaxLength;
        }

        private void btnCreateModule_Click(object sender, RoutedEventArgs e)
        {
            ModuleDataManager.CreateModule(Model.Name, Model.Tag, Model.Resref);
            ModuleDataManager.LoadModule(Model.Resref);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
