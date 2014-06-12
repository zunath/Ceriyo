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
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
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

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Model.ScriptNames = WorkingDataManager.GetAllScriptNames();
            Model.OpenScripts.Add(new GameScript("script0", "function Main()\n{\n\t\n}"));
            tcScripts.SelectedIndex = 0;
        }

        private void OpenScript(object sender, MouseButtonEventArgs e)
        {
            if (lbScripts.SelectedItem != null)
            {
                string scriptName = lbScripts.SelectedItem as string;

            }
        }

    }
}
