using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class DataEditorWindow : Window
    {
        private event EventHandler<EventArgs> OnSave;
        private event EventHandler<EventArgs> OnOpen;

        public DataEditorWindow()
        {
            InitializeComponent();
            SetUpEvents();
        }

        private void SetUpEvents()
        {
            OnSave += tilesetEditor.Save;
            OnSave += itemEditor.Save;
            OnSave += placeableEditor.Save;
            OnSave += animationEditor.Save;
            OnSave += creatureEditor.Save;

            OnOpen += tilesetEditor.Open;
            OnOpen += itemEditor.Open;
            OnOpen += placeableEditor.Open;
            OnOpen += animationEditor.Open;
            OnOpen += creatureEditor.Open;
        }

        public void Open()
        {
            if (OnOpen != null)
            {
                OnOpen(this, new EventArgs());
            }

            this.Show();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            if (OnSave != null)
            {
                OnSave(this, new EventArgs());
            }
            this.Hide();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            if (OnSave != null)
            {
                OnSave(this, new EventArgs());
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
