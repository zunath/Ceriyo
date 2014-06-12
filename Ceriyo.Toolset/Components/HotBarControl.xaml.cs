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
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for HotBarControl.xaml
    /// </summary>
    public partial class HotBarControl : UserControl
    {
        private ScriptEditorWindow ScriptEditor { get; set; }

        public HotBarControl()
        {
            InitializeComponent();
            this.ScriptEditor = new ScriptEditorWindow();
        }

        private void btnScriptEditor_Click(object sender, RoutedEventArgs e)
        {
            ScriptEditor.Open();
        }


    }
}
