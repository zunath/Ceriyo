using System.ComponentModel;
using System.Windows;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;
using Ceriyo.Entities.Screens;
using Ceriyo.Toolset.FRBControl;
using FlatRedBall;
using FlatRedBall.Screens;
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FRBGameComponent AreaEditorGame { get; set; }
        private ToolsetVM Model { get; set; }
        private EngineDataManager EngineManager { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
            Loaded += MainWindow_Loaded;
        }

        private void Initialize()
        {
            Model = new ToolsetVM();
            EngineManager = new EngineDataManager();
        }

        private void SetUpEvents()
        {
            AreaEditorGame = new FRBGameComponent(gameControl, typeof(AreaEditorScreen));
            AreaEditorScreen screen = ScreenManager.CurrentScreen as AreaEditorScreen;
            menuBar.OnOpenModule += OnModuleOpened;
            menuBar.OnOpenModule += areaSelection.ModuleLoaded;
            menuBar.OnDataEditorClosed += paintObjects.GameObjectsListsChanged;

            areaSelection.OnAreaOpen += objectSelection.LoadArea;
            areaSelection.OnAreaOpen += paintObjects.LoadArea;
            areaSelection.OnAreaOpen += screen.LoadArea;
            
            areaSelection.OnAreaSaved += screen.OnModulePropertiesUpdate;

            areaSelection.OnAreaClosed += paintObjects.UnloadArea;
            areaSelection.OnAreaClosed += screen.CloseArea;

            paintObjects.OnModeChange += screen.ChangePaintMode;
        }

        private void OnModuleOpened(object sender, GameModuleEventArgs e)
        {
            Model.Module = e.Module;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EngineManager.InitializeEngine();
            SetUpEvents();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
