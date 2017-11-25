using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ceriyo.Infrastructure.WPF.Helpers;

namespace Ceriyo.Toolset.WPF.Views.ScriptSelectorView
{
    /// <summary>
    /// Interaction logic for ScriptSelector
    /// </summary>
    public partial class ScriptSelector : UserControl
    {
        public ScriptSelector()
        {
            InitializeComponent();
        }
        
        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = TreeViewHelpers.VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (treeViewItem == null) return;

            treeViewItem.Focus();
            e.Handled = true;
        }
    }
}
