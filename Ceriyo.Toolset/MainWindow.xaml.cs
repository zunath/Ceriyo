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
        private AreaEditorGame AreaEditorGame { get; set; }
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
            areaSelection.OnAreaOpen += areaSelection_OnAreaOpen;
        }

        private void areaSelection_OnAreaOpen(object sender, GameObjectEventArgs e)
        {
            
        }

        private void menuBar_OnOpenModule(object sender, GameModuleEventArgs e)
        {
            Model.Module = e.Module;
            areaSelection.Populate();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AreaEditorGame = new AreaEditorGame(gameControl);
        }


    }
}
