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
        private event EventHandler<GameModuleEventArgs> OnModuleOpened;

        public AreaEditorComponent()
        {
            InitializeComponent();
        }

        private void SetUpInternalEvents()
        {
            AreaEditorScreen screen = ScreenManager.CurrentScreen as AreaEditorScreen;

            if (screen != null)
            {
                areaSelection.OnAreaOpen += objectSelection.LoadArea;
                areaSelection.OnAreaOpen += paintObjects.LoadArea;
                areaSelection.OnAreaOpen += screen.LoadArea;

                areaSelection.OnAreaSaved += screen.OnModulePropertiesUpdate;
                areaSelection.OnAreaSaved += paintObjects.ModulePropertiesUpdated;

                areaSelection.OnAreaClosed += paintObjects.UnloadArea;
                areaSelection.OnAreaClosed += screen.CloseArea;

                paintObjects.OnModeChange += screen.ChangePaintMode;
            }
        }

        private void SetUpExternalEvents()
        {
            OnDataEditorClosed += paintObjects.GameObjectsListsChanged;
            OnModuleOpened += areaSelection.ModuleLoaded;
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetUpInternalEvents();
            SetUpExternalEvents();
        }
    }
}
