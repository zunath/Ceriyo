using System;
using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;
using FlatRedBall.IO;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for SaveScriptWindow.xaml
    /// </summary>
    public partial class SaveScriptWindow
    {
        private SaveScriptVM Model { get; set; }
        public event EventHandler<ScriptEventArgs> OnSaveComplete;

        public SaveScriptWindow()
        {
            InitializeComponent();
            Model = new SaveScriptVM();
            DataContext = Model;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (FileManager.FileExists(WorkingPaths.ScriptsDirectory + Model.FileName + EnginePaths.ScriptExtension))
            {
                if (MessageBox.Show("A script with the name '" + Model.FileName + "' already exists. Do you want to overwrite it?", "Overwrite script?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (OnSaveComplete != null)
                    {
                        OnSaveComplete(this, new ScriptEventArgs(Model.FileName, Model.Contents, Model.OldFileName, true));
                    }

                    Hide();
                }
            }
            else
            {
                if (OnSaveComplete != null)
                {
                    OnSaveComplete(this, new ScriptEventArgs(Model.FileName, Model.Contents, Model.OldFileName));
                }

                Hide();
            }
        }

        public void Open(string fileName, string contents)
        {
            Model.FileName = fileName;
            Model.Contents = contents;
            Model.OldFileName = fileName;

            ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
