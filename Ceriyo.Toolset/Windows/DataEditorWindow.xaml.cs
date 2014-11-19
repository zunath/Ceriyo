using System;
using System.ComponentModel;
using System.Windows;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class DataEditorWindow
    {
        private event EventHandler<EventArgs> OnSave;
        private event EventHandler<EventArgs> OnOpen;
        public event EventHandler<EventArgs> OnWindowHidden;

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
            OnSave += abilityEditor.Save;
            OnSave += skillEditor.Save;
            OnSave += raceEditor.Save;
            OnSave += classEditor.Save;

            OnOpen += tilesetEditor.Open;
            OnOpen += itemEditor.Open;
            OnOpen += placeableEditor.Open;
            OnOpen += animationEditor.Open;
            OnOpen += creatureEditor.Open;
            OnOpen += abilityEditor.Open;
            OnOpen += skillEditor.Open;
            OnOpen += raceEditor.Open;
            OnOpen += classEditor.Open;

            animationEditor.OnAnimationsListChanged += creatureEditor.AnimationsModified;
            classEditor.OnClassesListChanged += creatureEditor.ClassesModified;
            classEditor.OnClassesListChanged += itemEditor.ClassesModified;
        }

        public void Open()
        {
            if (OnOpen != null)
            {
                OnOpen(this, new EventArgs());
            }

            Show();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            if (OnSave != null)
            {
                OnSave(this, new EventArgs());
            }
            Hide();

            if (OnWindowHidden != null)
            {
                OnWindowHidden(this, new EventArgs());
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Hide();

            if (OnWindowHidden != null)
            {
                OnWindowHidden(this, new EventArgs());
            }
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
            Hide();

            if (OnWindowHidden != null)
            {
                OnWindowHidden(this, new EventArgs());
            }
        }

    }
}
