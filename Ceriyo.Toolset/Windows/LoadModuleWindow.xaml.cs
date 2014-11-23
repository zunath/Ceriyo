using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for LoadModuleWindow.xaml
    /// </summary>
    public partial class LoadModuleWindow
    {
        private LoadModuleVM Model { get; set; }
        public event EventHandler<GameModuleEventArgs> OnOpenModule;
        private ModuleDataManager ModuleManager { get; set; }

        public LoadModuleWindow()
        {
            InitializeComponent();
            Model = new LoadModuleVM();
            ModuleManager = new ModuleDataManager();
            DataContext = Model;
            LoadFileNames();
        }

        private void LoadFileNames()
        {
            string[] files = Directory.GetFiles(EnginePaths.ModulesDirectory, "*" + EnginePaths.ModuleExtension);
            foreach (string file in files)
            {
                Model.Files.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Model.SelectedFile)) return;
            FileOperationResultTypeEnum result = ModuleManager.LoadModule(Model.SelectedFile);

            if (result == FileOperationResultTypeEnum.FileExists)
            {
                if (MessageBox.Show("WARNING: A module's temporary files are located on disk. If you continue, you will lose them. Are you sure you want to continue?", "Temporary Files Found", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    result = ModuleManager.LoadModule(Model.SelectedFile, true);
                }
                else
                {
                    return;
                }
            }

            if (result == FileOperationResultTypeEnum.FileDoesNotExist)
            {
                MessageBox.Show("Module could not be found.", "Warning", MessageBoxButton.OK);
            }
            else if (result == FileOperationResultTypeEnum.Failure)
            {
                MessageBox.Show("Failed to load module.", "Error", MessageBoxButton.OK);
            }
            else if (result == FileOperationResultTypeEnum.Success)
            {
                Close();

                if (OnOpenModule != null)
                {
                    GameModule moduleToLoad = ModuleManager.GetGameModule(Model.SelectedFile);
                    OnOpenModule(this, new GameModuleEventArgs(moduleToLoad));
                }
            }
        }

        private void lbModuleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbModuleList.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
