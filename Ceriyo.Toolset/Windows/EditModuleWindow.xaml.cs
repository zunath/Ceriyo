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

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for NewModuleWindow.xaml
    /// </summary>
    public partial class EditModuleWindow : Window
    {
        public EditModuleWindow()
        {
            InitializeComponent();
        }

        public EditModuleWindow(string title)
        {
            InitializeComponent();
            this.Title = title;
            txtName.Focus();
            txtName.MaxLength = EngineConstants.NameMaxLength;
            txtTag.MaxLength = EngineConstants.TagMaxLength;
            txtResref.MaxLength = EngineConstants.ResrefMaxLength;
        }

        private void btnCreateModule_Click(object sender, RoutedEventArgs e)
        {
            ModuleDataManager.CreateModule(txtName.Text, txtTag.Text, txtResref.Text);
            ModuleDataManager.LoadModule(txtResref.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
