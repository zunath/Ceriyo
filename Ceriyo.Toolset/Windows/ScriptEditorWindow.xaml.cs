using System.Linq;
using System.Windows;
using System.Windows.Input;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using FlatRedBall.IO;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ScriptEditorWindow.xaml
    /// </summary>
    public partial class ScriptEditorWindow
    {
        private ScriptEditorVM Model { get; set; }
        private SaveScriptWindow SaveScriptWin { get; set; }
        private const string DefaultScriptText = "";

        public ScriptEditorWindow()
        {
            InitializeComponent();
            Model = new ScriptEditorVM();
            SaveScriptWin = new SaveScriptWindow();
            SaveScriptWin.OnSaveComplete += SaveScriptWin_OnSaveComplete;
            DataContext = Model;
        }

        private void SaveScript(object sender, RoutedEventArgs e)
        {
            GameScript script = Model.OpenScripts[tcScripts.SelectedIndex];

            if (FileManager.FileExists(WorkingPaths.ScriptsDirectory + script.Name + EnginePaths.ScriptExtension))
            {
                DoScriptSave(script.Name, script.ScriptDocument.Text);
            }
            else
            {
                SaveScriptWin.Open(script.Name, script.ScriptDocument.Text);
            }
        }

        private void SaveScriptWin_OnSaveComplete(object sender, ScriptEventArgs e)
        {
            DoScriptSave(e.Name, e.Contents);
            Model.ScriptNames = WorkingDataManager.GetAllScriptNames(false);
            GameScript existingScript = Model.OpenScripts.SingleOrDefault(x => x.Name == e.OldName);
            Model.OpenScripts.Remove(existingScript);
            
            if (e.IsOverwrite)
            {
                existingScript = Model.OpenScripts.SingleOrDefault(x => x.Name == e.Name);
                Model.OpenScripts.Remove(existingScript);
            }

            existingScript = new GameScript(e.Name, e.Contents);
            Model.OpenScripts.Add(existingScript);
            tcScripts.SelectedIndex = Model.OpenScripts.IndexOf(existingScript);
            
        }

        private void DoScriptSave(string fileName, string contents)
        {
            FileManager.SaveText(contents, WorkingPaths.ScriptsDirectory + fileName + EnginePaths.ScriptExtension);
        }

        private void SaveAllScripts(object sender, RoutedEventArgs e)
        {
        }

        private void NewScript(object sender, RoutedEventArgs e)
        {
            GameScript script = new GameScript("script" + GetUniqueScriptID(), DefaultScriptText);
            Model.OpenScripts.Add(script);
            tcScripts.SelectedIndex = Model.OpenScripts.IndexOf(script);
        }

        public void Open()
        {
            Model.ScriptNames.Clear();
            Model.OpenScripts.Clear();

            Model.ScriptNames = WorkingDataManager.GetAllScriptNames(false);
            Model.OpenScripts.Add(new GameScript("script" + GetUniqueScriptID(), DefaultScriptText));
            tcScripts.SelectedIndex = 0;
            Show();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Model.OpenScripts.Clear();
            Model.ScriptNames.Clear();
            Hide();
        }

        private int GetUniqueScriptID()
        {
            int scriptID = 0;

            while (Model.OpenScripts.SingleOrDefault(x => x.Name == "script" + scriptID) != null ||
                  Model.ScriptNames.Contains("script" + scriptID))
            {
                scriptID++;
            }

            return scriptID;
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
                GameScript script = new GameScript
                {
                    Name = lbScripts.SelectedItem as string
                };
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

            if (scriptName == null) return;
            if (MessageBox.Show("Are you sure you want to delete the script " + scriptName + " ?", "Delete Script?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            FileOperationResultTypeEnum result = WorkingDataManager.DeleteScript(scriptName);

            switch (result)
            {
                case FileOperationResultTypeEnum.Success:
                    Model.ScriptNames.Remove(scriptName);
                    GameScript existingScript = Model.OpenScripts.SingleOrDefault(x => x.Name == scriptName);
                    if (existingScript != null)
                    {
                        Model.OpenScripts.Remove(existingScript);
                    }
                    break;
                case FileOperationResultTypeEnum.FileDoesNotExist:
                    MessageBox.Show("Unable to delete script. File does not exist.", "Unable to delete script", MessageBoxButton.OK);
                    break;
                case FileOperationResultTypeEnum.Failure:
                    MessageBox.Show("Unable to delete script. Deletion failed.", "Unable to delete script", MessageBoxButton.OK);
                    break;
            }
        }

        private void btnCloseScript_Click(object sender, RoutedEventArgs e)
        {
            Model.OpenScripts.RemoveAt(tcScripts.SelectedIndex);

            if (Model.OpenScripts.Count > 0) return;
            Model.OpenScripts.Add(new GameScript("script" + GetUniqueScriptID(), DefaultScriptText));
            tcScripts.SelectedIndex = 0;
        }

    }
}
