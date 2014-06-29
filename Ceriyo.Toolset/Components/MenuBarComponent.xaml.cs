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
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for MenuBarComponent.xaml
    /// </summary>
    public partial class MenuBarComponent : UserControl
    {
        private ResourcePackEditorWindow ResourceEditor { get; set; }
        private ManageResourcePacksWindow ResourceManager { get; set; }
        private WorkingDataManager WorkingManager { get; set; }
        private ResourcePackDataManager ResourcePackManager { get; set; }
        private ModulePropertiesWindow ModuleProperties { get; set; }
        private DataEditorWindow DataEditor { get; set; }
        public event EventHandler<GameModuleEventArgs> OnOpenModule;
        public event EventHandler<EventArgs> OnDataEditorClosed;

        public MenuBarComponent()
        {
            InitializeComponent();
            ModuleProperties = new ModulePropertiesWindow();
            ResourceEditor = new ResourcePackEditorWindow();
            ResourcePackManager = new ResourcePackDataManager();
            WorkingManager = new WorkingDataManager();
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
            NewModuleWindow modWindow = new NewModuleWindow();
            modWindow.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoadModuleWindow loadWindow = new LoadModuleWindow();
            loadWindow.OnOpenModule += OpenModuleFinished;
            loadWindow.ShowDialog();
        }

        private void OpenModuleFinished(object sender, GameModuleEventArgs e)
        {
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
            GameModule module = WorkingManager.GetGameModule();
            bool success = ResourcePackManager.BuildModule(module.ResourcePacks);

            if (success)
            {
                MessageBox.Show("Module built successfully!", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Module failed to build.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
