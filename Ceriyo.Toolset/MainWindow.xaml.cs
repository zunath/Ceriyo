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
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToolsetGame Game { get; set; }
        private ToolsetVM Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Model = new ToolsetVM();
            SetUpEvents();
        }

        private void SetUpEvents()
        {
            Loaded += MainWindow_Loaded;
            menuBar.OnOpenModule += menuBar_OnOpenModule;
        }

        private void menuBar_OnOpenModule(object sender, GameModuleEventArgs e)
        {
            Model.Module = e.Module;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //_game = new ToolsetGame(flatRedBallControl);
        }


    }
}
