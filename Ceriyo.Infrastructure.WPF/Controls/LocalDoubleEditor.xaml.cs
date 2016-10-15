using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Ceriyo.Core.Data;

namespace Ceriyo.Infrastructure.WPF.Controls
{
    /// <summary>
    /// Interaction logic for LocalFloatEditor.xaml
    /// </summary>
    public partial class LocalDoubleEditor
    {
        public LocalDoubleEditor()
        {
            InitializeComponent();
            ItemsListView.DataContext = ItemsSource;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value);}
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LocalDoubleEditor));
        
        
        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            LocalDoubleData data = (LocalDoubleData) button.DataContext;

            IList<LocalDoubleData> list = (IList<LocalDoubleData>) ItemsSource;
            list.Remove(data);
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            IList<LocalDoubleData> list = (IList<LocalDoubleData>) ItemsSource;
            list.Add(new LocalDoubleData());
        }
    }
}
