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
using FlatRedBall.IO;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for NewModuleWindow.xaml
    /// </summary>
    public partial class NewModuleWindow : Window
    {
        public NewModuleWindow()
        {
            InitializeComponent();
        }

        public NewModuleWindow(string title)
        {
            InitializeComponent();
            this.Title = title;
            txtName.Focus();
        }

        private void btnCreateModule_Click(object sender, RoutedEventArgs e)
        {
            string folder = FileManager.RelativeDirectory + @"Content/" + ConfigurationManager.AppSettings["ToolsetFolder_WorkingDirectory"];

            try
            {
                if (Directory.Exists(folder))
                {
                    Directory.Delete(folder);
                }

                Directory.CreateDirectory(folder);
            }
            catch
            {
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
