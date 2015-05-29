using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
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

        public LoadModuleWindow()
        {
            InitializeComponent();
            Model = new LoadModuleVM();
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
            FileOperationResultType result = ModuleDataManager.LoadModule(Model.SelectedFile);

            if (result == FileOperationResultType.FileExists)
            {
                if (MessageBox.Show("WARNING: A module's temporary files are located on disk. If you continue, you will lose them. Are you sure you want to continue?", "Temporary Files Found", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    result = ModuleDataManager.LoadModule(Model.SelectedFile, true);
                }
                else
                {
                    return;
                }
            }

            if (result == FileOperationResultType.FileDoesNotExist)
            {
                MessageBox.Show("Module could not be found.", "Warning", MessageBoxButton.OK);
            }
            else if (result == FileOperationResultType.Failure)
            {
                MessageBox.Show("Failed to load module.", "Error", MessageBoxButton.OK);
            }
            else if (result == FileOperationResultType.Success)
            {
                Close();

                if (OnOpenModule != null)
                {
                    OnOpenModule(this, new GameModuleEventArgs(Model.SelectedFile));
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
