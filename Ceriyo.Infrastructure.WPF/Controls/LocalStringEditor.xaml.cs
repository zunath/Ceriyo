using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

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
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                
            }
        }

        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
        }
    }
}
