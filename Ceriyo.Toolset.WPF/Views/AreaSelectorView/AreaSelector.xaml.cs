using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ceriyo.Infrastructure.WPF.Helpers;

namespace Ceriyo.Toolset.WPF.Views.AreaSelectorView
{
    public partial class AreaSelector
    {
        public AreaSelector()
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
