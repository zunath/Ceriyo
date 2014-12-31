using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for SaveModuleWindow.xaml
    /// </summary>
    public partial class SaveModuleWindow
    {
        private SaveModuleVM Model { get; set; }
        public event EventHandler<GameModuleEventArgs> OnSaveModule;

        public SaveModuleWindow()
        {
            InitializeComponent();
            Model = new SaveModuleVM();
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool fileExists = Model.Files.SingleOrDefault(x => x == Model.FileName + EnginePaths.ModuleExtension) != null;

            if (string.IsNullOrWhiteSpace(Model.FileName))
            {
                MessageBox.Show("Please enter in a valid file name.", "Invalid file name", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else if (fileExists)
            {
                var result = MessageBox.Show(
                    "A module with that file name already exists. Do you want to overwrite it?",
                    "File already exists", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (OnSaveModule != null)
                    {
                        OnSaveModule(this, new GameModuleEventArgs(Model.FileName));
                    }

                    Close();
                }

            }
            else
            {
                ModuleDataManager.SaveModule(Model.FileName);
                Close();
            }
            
        }

        private void lbModuleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbModuleList.SelectedItem != null)
            {
                btnSave.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ModuleSelected(object sender, SelectionChangedEventArgs e)
        {
            Model.FileName = Path.GetFileNameWithoutExtension(Model.SelectedFile);
        }
    }
}
