using System.ComponentModel;
using System.Windows;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;
using Ceriyo.Toolset.GameComponents;

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
            Initialize();
            SetUpEvents();
        }



        private void Initialize()
        {
            Model = new ToolsetVM();
        }

        private void SetUpEvents()
        {
            Loaded += MainWindow_Loaded;
            menuBar.OnOpenModule += menuBar_OnOpenModule;
            areaSelection.OnAreaOpen += objectSelection.LoadArea;
        }

        private void menuBar_OnOpenModule(object sender, GameModuleEventArgs e)
        {
            Model.Module = e.Module;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EngineDataManager.InitializeEngine();
            AreaEditorGame = new AreaEditorGame(gameControl);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
