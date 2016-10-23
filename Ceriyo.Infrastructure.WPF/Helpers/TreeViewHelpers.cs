using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ceriyo.Infrastructure.WPF.Helpers
{
    public class TreeViewHelpers
    {
        public static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }
    }
}
