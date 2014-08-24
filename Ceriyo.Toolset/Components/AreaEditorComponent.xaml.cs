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
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.Screens;
using FlatRedBall.Screens;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AreaEditorComponent.xaml
    /// </summary>
    public partial class AreaEditorComponent : UserControl
    {
        private event EventHandler<EventArgs> OnDataEditorClosed;
        public event EventHandler<GameModuleEventArgs> OnModuleOpened;
        private event EventHandler<EventArgs> OnAreaClosed;
        private event EventHandler<AreaPropertiesChangedEventArgs> OnAreaPropertiesSaved;
        private event EventHandler<GameObjectEventArgs> OnAreaOpened;
        private event EventHandler<EventArgs> OnAreaSaved;

        public AreaEditorComponent()
        {
            InitializeComponent();
        }

        private void SetUpInternalEvents()
        {
            AreaEditorScreen screen = ScreenManager.CurrentScreen as AreaEditorScreen;

            if (screen != null)
            {
                OnAreaOpened += objectSelection.LoadArea;
                OnAreaOpened += paintObjects.LoadArea;
                OnAreaOpened += screen.LoadArea;

                OnAreaPropertiesSaved += screen.OnAreaPropertiesUpdate;
                OnAreaPropertiesSaved += paintObjects.AreaPropertiesUpdated;

                OnAreaClosed += paintObjects.UnloadArea;
                OnAreaClosed += screen.CloseArea;

                paintObjects.OnModeChange += screen.ChangePaintMode;

                OnAreaSaved += screen.SaveArea;
            }
        }

        private void SetUpExternalEvents()
        {
            OnDataEditorClosed += paintObjects.GameObjectsListsChanged;
        }

        public void DataEditorClosed(object sender, EventArgs e)
        {
            if (OnDataEditorClosed != null)
            {
                OnDataEditorClosed(sender, e);
            }
        }

        public void ModuleOpened(object sender, GameModuleEventArgs e)
        {
            if (OnModuleOpened != null)
            {
                OnModuleOpened(sender, e);
            }
        }

        public void AreaSaved(object sender, EventArgs e)
        {
            if (OnAreaSaved != null)
            {
                OnAreaSaved(sender, e);
            }
        }

        public void AreaOpened(object sender, GameObjectEventArgs e)
        {
            if (OnAreaOpened != null)
            {
                OnAreaOpened(sender, e);
            }
        }

        public void AreaClosed(object sender, EventArgs e)
        {
            if (OnAreaClosed != null)
            {
                OnAreaClosed(sender, e);
            }
        }

        public void AreaPropertiesSaved(object sender, AreaPropertiesChangedEventArgs e)
        {
            if (OnAreaPropertiesSaved != null)
            {
                OnAreaPropertiesSaved(sender, e);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetUpInternalEvents();
            SetUpExternalEvents();
        }
    }
}
