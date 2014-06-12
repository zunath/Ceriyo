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
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using FlatRedBall.IO;
using ICSharpCode;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ScriptEditorWindow.xaml
    /// </summary>
    public partial class ScriptEditorWindow : Window
    {
        private ScriptEditorVM Model { get; set; }

        public ScriptEditorWindow()
        {
            InitializeComponent();
            Model = new ScriptEditorVM();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            lbScripts.DataContext = Model;
            lbMethods.DataContext = Model;
            lbConstants.DataContext = Model;
            tcScripts.DataContext = Model;
        }

        private void SaveScript(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAllScripts(object sender, RoutedEventArgs e)
        {
        }

        private void NewScript(object sender, RoutedEventArgs e)
        {
            GameScript script = new GameScript("newscript", "function Main()\n{\n\t\n}");
            Model.OpenScripts.Add(script);
            tcScripts.SelectedIndex = Model.OpenScripts.IndexOf(script);
        }

        public void Open()
        {
            Model.ScriptNames.Clear();
            Model.OpenScripts.Clear();

            Model.ScriptNames = WorkingDataManager.GetAllScriptNames();
            Model.OpenScripts.Add(new GameScript("script0", "function Main()\n{\n\t\n}"));
            tcScripts.SelectedIndex = 0;
            this.Show();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Model.OpenScripts.Clear();
            Model.ScriptNames.Clear();
            this.Hide();
        }

        private void OpenScript(object sender, MouseButtonEventArgs e)
        {
            DoOpenScript();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Close(this, null);
        }

        private void miOpenScript_Click(object sender, RoutedEventArgs e)
        {
            DoOpenScript();
        }

        private void DoOpenScript()
        {
            if (lbScripts.SelectedItem != null)
            {
                GameScript script = new GameScript();
                script.Name = lbScripts.SelectedItem as string;
                GameScript existingScript = Model.OpenScripts.SingleOrDefault(x => x.Name == script.Name);

                if (existingScript == null)
                {
                    script.ScriptDocument.Text = FileManager.FromFileText(WorkingPaths.ScriptsDirectory + script.Name + EnginePaths.ScriptExtension);
                    Model.OpenScripts.Add(script);
                    tcScripts.SelectedIndex = Model.OpenScripts.IndexOf(script);
                }
                else
                {
                    tcScripts.SelectedIndex = Model.OpenScripts.IndexOf(existingScript);
                }
            }
        }

        private void miDeleteScript_Click(object sender, RoutedEventArgs e)
        {
            string scriptName = lbScripts.SelectedItem as string;

            if (scriptName != null)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete the script " + scriptName + " ?", "Delete Script?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        FileOperationResultTypeEnum result = WorkingDataManager.DeleteScript(scriptName);

                        if (result == FileOperationResultTypeEnum.Success)
                        {
                            Model.ScriptNames.Remove(scriptName);

                            GameScript existingScript = Model.OpenScripts.SingleOrDefault(x => x.Name == scriptName);
                            if (existingScript != null)
                            {
                                Model.OpenScripts.Remove(existingScript);
                            }
                        }
                        else if (result == FileOperationResultTypeEnum.FileDoesNotExist)
                        {
                            MessageBox.Show("Unable to delete script. File does not exist.", "Unable to delete script", MessageBoxButton.OK);
                        }
                        else if (result == FileOperationResultTypeEnum.Failure)
                        {
                            MessageBox.Show("Unable to delete script. Deletion failed.", "Unable to delete script", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
