using System;
using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for MenuBarComponent.xaml
    /// </summary>
    public partial class MenuBarComponent
    {
        private MenuBarVM Model { get; set; }
        private ResourcePackEditorWindow ResourceEditor { get; set; }
        private ManageResourcePacksWindow ResourceManager { get; set; }
        private ModulePropertiesWindow ModuleProperties { get; set; }
        private DataEditorWindow DataEditor { get; set; }
        public event EventHandler<GameModuleEventArgs> OnOpenModule;
        public event EventHandler<EventArgs> OnSaveModule;
        public event EventHandler<EventArgs> OnCloseModule;
        public event EventHandler<EventArgs> OnDataEditorClosed;

        public MenuBarComponent()
        {
            InitializeComponent();
            Model = new MenuBarVM();
            DataContext = Model;

            ModuleProperties = new ModulePropertiesWindow();
            ResourceEditor = new ResourcePackEditorWindow();
            ResourceManager = new ManageResourcePacksWindow();
            DataEditor = new DataEditorWindow();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            DataEditor.OnWindowHidden += RaiseDataEditorClosedEvent;
            
        }

        private void RaiseDataEditorClosedEvent(object sender, EventArgs e)
        {
            if (OnDataEditorClosed != null)
            {
                OnDataEditorClosed(this, e);
            }
        }

        private void NewModule_Click(object sender, RoutedEventArgs e)
        {
            NewModuleWindow modWindow = new NewModuleWindow
            {
                Owner = Window.GetWindow(this)
            };

            modWindow.OnModuleCreated += ModuleCreated;

            modWindow.ShowDialog();
        }

        private void ModuleCreated(object sender, GameModuleEventArgs eventArgs)
        {
            ModuleDataManager.LoadModule(eventArgs.FileName, true);
            OpenModuleFinished(sender, eventArgs);
        }

        private void OpenModule(object sender, RoutedEventArgs e)
        {
            LoadModuleWindow loadWindow = new LoadModuleWindow
            {
                Owner = Window.GetWindow(this)
            };

            loadWindow.OnOpenModule += OpenModuleFinished;
            loadWindow.ShowDialog();
        }

        private void OpenModuleFinished(object sender, GameModuleEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.FileName))
            {
                Model.IsModuleLoaded = true;
            }

            if(OnOpenModule != null)
            {
                OnOpenModule(sender, e);
            }

            RaiseDataEditorClosedEvent(this, new EventArgs());
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenModuleProperties(object sender, RoutedEventArgs e)
        {
            ModuleProperties.Open();
        }

        private void OpenResourceEditor(object sender, RoutedEventArgs e)
        {
            ResourceEditor.Open();
        }

        private void OpenResourceManager(object sender, RoutedEventArgs e)
        {
            ResourceManager.Open();
        }

        private void OpenDataEditor(object sender, RoutedEventArgs e)
        {
            DataEditor.Open();
        }

        private void BuildModule(object sender, RoutedEventArgs e)
        {
            GameModule module = WorkingDataManager.GetGameModule();
            bool success = ResourcePackDataManager.BuildModule(module.ResourcePacks);

            if (success)
            {
                MessageBox.Show("Module built successfully!", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Module failed to build.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveModule(object sender, RoutedEventArgs e)
        {
            GameModule module = WorkingDataManager.GetGameModule();
            DoSaveModule(sender, new GameModuleEventArgs(module.Resref));
        }

        private void SaveAsModule(object sender, RoutedEventArgs e)
        {
            SaveModuleWindow window = new SaveModuleWindow
            {
                Owner = Window.GetWindow(this)
            };
            window.OnSaveModule += DoSaveModule;

            window.ShowDialog();
        }

        private void DoSaveModule(object sender, GameModuleEventArgs e)
        {
            if (OnSaveModule != null)
            {
                OnSaveModule(this, new EventArgs());
            }

            ModuleDataManager.SaveModule(e.FileName);
        }

        private void MenuBarComponent_OnLoaded(object sender, RoutedEventArgs e)
        {
            ResourceEditor.Owner = Window.GetWindow(this);
            ResourceManager.Owner = Window.GetWindow(this);
            ModuleProperties.Owner = Window.GetWindow(this);
            DataEditor.Owner = Window.GetWindow(this);
        }

        private void CloseModule(object sender, RoutedEventArgs e)
        {
            var result = ModuleDataManager.CloseModule();

            if (result == FileOperationResultTypeEnum.Success)
            {
                Model.IsModuleLoaded = false;

                if (OnCloseModule != null)
                {
                    OnCloseModule(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Unable to close module.", "Failed to close module", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void AreaClosed(object sender, EventArgs e)
        {
            Model.IsAreaLoaded = false;
        }

        public void AreaOpened(object sender, GameObjectEventArgs e)
        {
            Model.IsAreaLoaded = e.GameObject != null;
        }
    }
}
