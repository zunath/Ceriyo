﻿using System.Collections;
using System.Collections.Generic;
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
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LocalStringEditor));
        
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
