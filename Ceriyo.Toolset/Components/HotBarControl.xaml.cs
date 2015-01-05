using System;
using System.Windows;
using System.Windows.Controls;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for HotBarControl.xaml
    /// </summary>
    public partial class HotBarControl
    {
        public event EventHandler<SimpleTypesEventArgs> OnLayerChanged;
        
        private HotBarVM Model { get; set; }
        private ScriptEditorWindow ScriptEditor { get; set; }

        public HotBarControl()
        {
            InitializeComponent();
            Model = new HotBarVM();
            DataContext = Model;

            ScriptEditor = new ScriptEditorWindow();
        }

        private void btnScriptEditor_Click(object sender, RoutedEventArgs e)
        {
            ScriptEditor.Open();
        }

        private void btnDialogEditor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStartLocation_Click(object sender, RoutedEventArgs e)
        {

        }


        public void ModuleOpened(object sender, GameModuleEventArgs e)
        {
            Model.IsModuleLoaded = true;
        }

        public void ModuleClosed(object sender, EventArgs e)
        {
            Model.IsModuleLoaded = false;
        }

        public void AreaOpened(object sender, GameObjectEventArgs e)
        {
            Model.IsAreaLoaded = true;
        }

        public void AreaClosed(object sender, EventArgs e)
        {
            Model.IsAreaLoaded = false;
        }

        private void ChangeLayer(object sender, SelectionChangedEventArgs e)
        {
            if (OnLayerChanged != null)
            {
                OnLayerChanged(this, new SimpleTypesEventArgs(Model.SelectedLayer.Layer));
            }
        }
    }
}
