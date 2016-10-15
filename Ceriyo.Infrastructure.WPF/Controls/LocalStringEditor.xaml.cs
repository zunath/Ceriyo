using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Ceriyo.Core.Data;

namespace Ceriyo.Infrastructure.WPF.Controls
{
    /// <summary>
    /// Interaction logic for LocalStringEditor.xaml
    /// </summary>
    public partial class LocalStringEditor
    {
        public LocalStringEditor()
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
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LocalStringEditor), new PropertyMetadata(OnItemsSourcePropertyChanged));


        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LocalStringEditor;
            control?.OnItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
        }



        private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            // Remove handler for oldValue.CollectionChanged
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;

            if (null != oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.CollectionChanged -= newValueINotifyCollectionChanged_CollectionChanged;
            }
            // Add handler for newValue.CollectionChanged (if possible)
            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (null != newValueINotifyCollectionChanged)
            {
                newValueINotifyCollectionChanged.CollectionChanged += newValueINotifyCollectionChanged_CollectionChanged;
            }

        }

        private void newValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            LocalStringData data = (LocalStringData) button.DataContext;

            IList<LocalStringData> list = (IList<LocalStringData>) ItemsSource;
            list.Remove(data);
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            IList<LocalStringData> list = (IList<LocalStringData>) ItemsSource;
            list.Add(new LocalStringData());
        }
    }
}
