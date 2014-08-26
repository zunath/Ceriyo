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
using System.Windows.Shapes;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;
using FlatRedBall.IO;
using Ceriyo.Data;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for SaveScriptWindow.xaml
    /// </summary>
    public partial class SaveScriptWindow : Window
    {
        private SaveScriptVM Model { get; set; }
        public event EventHandler<ScriptEventArgs> OnSaveComplete;

        public SaveScriptWindow()
        {
            InitializeComponent();
            Model = new SaveScriptVM();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            txtScriptName.DataContext = Model;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FileManager.FileExists(WorkingPaths.ScriptsDirectory + Model.FileName + EnginePaths.ScriptExtension))
                {
                    if (MessageBox.Show("A script with the name '" + Model.FileName + "' already exists. Do you want to overwrite it?", "Overwrite script?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        if (OnSaveComplete != null)
                        {
                            OnSaveComplete(this, new ScriptEventArgs(Model.FileName, Model.Contents, Model.OldFileName, true));
                        }

                        this.Hide();
                    }
                }
                else
                {
                    if (OnSaveComplete != null)
                    {
                        OnSaveComplete(this, new ScriptEventArgs(Model.FileName, Model.Contents, Model.OldFileName, false));
                    }

                    this.Hide();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Open(string fileName, string contents)
        {
            Model.FileName = fileName;
            Model.Contents = contents;
            Model.OldFileName = fileName;

            this.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
