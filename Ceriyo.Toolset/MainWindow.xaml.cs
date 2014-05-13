using System.ComponentModel;
using System.Windows;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;
using Ceriyo.Entities.Screens;
using Ceriyo.Toolset.FRBControl;

namespace Ceriyo.Toolset
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FRBGameComponent AreaEditorGame { get; set; }
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
            menuBar.OnOpenModule += OnModuleOpened;
            areaSelection.OnAreaOpen += objectSelection.LoadArea;
            areaSelection.OnAreaOpen += paintObjects.LoadArea;
        }

        private void OnModuleOpened(object sender, GameModuleEventArgs e)
        {
            Model.Module = e.Module;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EngineDataManager.InitializeEngine();
            AreaEditorGame = new FRBGameComponent(gameControl, typeof(AreaEditorScreen));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
