using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Settings;
using Ceriyo.Data.ViewModels;
using Ceriyo.Entities.Screens;
using Ceriyo.Toolset.FRBControl;
using FlatRedBall.IO;
using System;
using System.ComponentModel;
using System.Windows;

namespace Ceriyo.Toolset
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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
            DataContext = Model;

            LoadSettings();
        }

        private void SetUpEvents()
        {
            AreaEditorGame = new FRBGameComponent(gameControl, typeof(AreaEditorScreen));
            menuBar.OnOpenModule += OnModuleOpened;
            menuBar.OnOpenModule += areaSelection.ModuleLoaded;
            menuBar.OnOpenModule += hotBar.ModuleOpened;

            menuBar.OnSaveModule += areaSelection.ModuleSaved;

            menuBar.OnCloseModule += OnModuleClosed;
            menuBar.OnCloseModule += areaSelection.ModuleClosed;
            menuBar.OnCloseModule += hotBar.ModuleClosed;

            menuBar.OnDataEditorClosed += areaEditor.DataEditorClosed;

            areaSelection.OnAreaOpen += areaEditor.AreaOpened;
            areaSelection.OnAreaOpen += menuBar.AreaOpened;
            areaSelection.OnAreaOpen += hotBar.AreaOpened;

            areaSelection.OnAreaPropertiesSaved += areaEditor.AreaPropertiesSaved;
            
            areaSelection.OnAreaClosed += areaEditor.AreaClosed;
            areaSelection.OnAreaClosed += menuBar.AreaClosed;
            areaSelection.OnAreaClosed += hotBar.AreaClosed;

            areaSelection.OnAreaSaved += areaEditor.AreaSaved;
        }

        private void OnModuleOpened(object sender, GameModuleEventArgs e)
        {
            Model.WindowTitle = "Ceriyo Editor - " + e.FileName + EnginePaths.ModuleExtension;
        }

        private void OnModuleClosed(object sender, EventArgs e)
        {
            Model.WindowTitle = "Ceriyo Editor";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EngineManager.InitializeEngine();
            SetUpEvents();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveSettings();

            Application.Current.Shutdown();
        }

        private void SaveSettings()
        {
            string path = EnginePaths.SettingsDirectory + "ToolsetSettings" + EnginePaths.DataExtension;
            ToolsetSettings settings = new ToolsetSettings
            {
                MainWindowHeight = Convert.ToInt32(Height),
                MainWindowWidth = Convert.ToInt32(Width)
            };

            FileManager.XmlSerialize(settings, path);
        }

        private void LoadSettings()
        {
            string path = EnginePaths.SettingsDirectory + "ToolsetSettings" + EnginePaths.DataExtension;

            if (!FileManager.FileExists(path)) return;
            ToolsetSettings settings = FileManager.XmlDeserialize<ToolsetSettings>(path);

            Width = settings.MainWindowWidth;
            Height = settings.MainWindowHeight;
        }
    }
}
