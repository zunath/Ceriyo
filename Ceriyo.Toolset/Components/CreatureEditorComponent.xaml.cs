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
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for CreatureEditorComponent.xaml
    /// </summary>
    public partial class CreatureEditorComponent : UserControl
    {
        private CreatureEditorVM Model { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public CreatureEditorComponent()
        {
            InitializeComponent();
            Model = new CreatureEditorVM();
            WorkingManager = new WorkingDataManager();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            lbCreatures.DataContext = Model;
            txtName.DataContext = Model;
            txtTag.DataContext = Model;
            txtResref.DataContext = Model;
            lbClass.DataContext = Model;
            dgLocalVariables.DataContext = Model;
            txtDescription.DataContext = Model;
            txtComments.DataContext = Model;

            ddlOnAttackedScript.DataContext = Model;
            ddlOnConversationScript.DataContext = Model;
            ddlOnDamagedScript.DataContext = Model;
            ddlOnDeathScript.DataContext = Model;
            ddlOnDisturbedScript.DataContext = Model;
            ddlOnHeartbeatScript.DataContext = Model;
            ddlOnSpawnedScript.DataContext = Model;
        }

        private void CreatureSelected(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        private void New(object sender, RoutedEventArgs e)
        {

        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Creatures.Cast<IGameObject>().ToList(), ModulePaths.CreaturesDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save creatures.", "Saving creatures failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Creatures = WorkingManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
            Model.Dialogs = WorkingManager.GetAllGameObjects<Dialog>(ModulePaths.DialogsDirectory);
            Model.Scripts = WorkingManager.GetAllScriptNames();
        }

        private void ClassSelected(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
