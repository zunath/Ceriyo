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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for CreatureEditorComponent.xaml
    /// </summary>
    public partial class CreatureEditorComponent : UserControl
    {
        private CreatureEditorVM Model { get; set; }
        private WorkingDataManager WorkingManager { get; set; }
        private GameResourceProcessor Processor { get; set; }

        public CreatureEditorComponent()
        {
            InitializeComponent();
            Model = new CreatureEditorVM();
            WorkingManager = new WorkingDataManager();
            Processor = new GameResourceProcessor();
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
            numLevel.DataContext = Model;
            ddlDialog.DataContext = Model;

            ddlOnAttackedScript.DataContext = Model;
            ddlOnConversationScript.DataContext = Model;
            ddlOnDamagedScript.DataContext = Model;
            ddlOnDeathScript.DataContext = Model;
            ddlOnDisturbedScript.DataContext = Model;
            ddlOnHeartbeatScript.DataContext = Model;
            ddlOnSpawnedScript.DataContext = Model;

            ddlMoveEastAnimation.DataContext = Model;
            ddlMoveNorthAnimation.DataContext = Model;
            ddlMoveNortheastAnimation.DataContext = Model;
            ddlMoveNorthwestAnimation.DataContext = Model;
            ddlMoveSouthAnimation.DataContext = Model;
            ddlMoveSoutheastAnimation.DataContext = Model;
            ddlMoveSouthwestAnimation.DataContext = Model;
            ddlMoveWestAnimation.DataContext = Model;
        }

        private void CreatureSelected(object sender, SelectionChangedEventArgs e)
        {
            Creature creature = lbCreatures.SelectedItem as Creature;
            Model.SelectedCreature = creature;
            Model.IsCreatureSelected = creature == null ? false : true;

            if (creature != null)
            {
                if (string.IsNullOrWhiteSpace(creature.CharacterClassResref))
                {
                    lbClass.SelectedItem = lbClass.Items[0];
                }
                else
                {
                    lbClass.SelectedItem = Model.CharacterClasses.SingleOrDefault(x => x.Resref == creature.CharacterClassResref);
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedCreature != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this creature?", "Delete creature?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Creatures.Remove(Model.SelectedCreature);
                    Model.SelectedCreature = null;
                    Model.IsCreatureSelected = false;
                }
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Creature creature = new Creature();
            string resref = Processor.GenerateUniqueResref(Model.Creatures.Cast<IGameObject>().ToList(), creature.CategoryName);

            creature.Name = resref;
            creature.Tag = resref;
            creature.Resref = resref;
            creature.CharacterClassResref = Model.CharacterClasses[0].Resref;

            Model.Creatures.Add(creature);
            int index = Model.Creatures.IndexOf(creature);
            Model.SelectedCreature = Model.Creatures[index];
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Creatures.Cast<IGameObject>().ToList(), WorkingPaths.CreaturesDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save creatures.", "Saving creatures failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Animations = WorkingManager.GetAllGameObjects<SpriteAnimation>(ModulePaths.AnimationsDirectory);
            Model.Creatures = WorkingManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
            Model.Dialogs = WorkingManager.GetAllGameObjects<Dialog>(ModulePaths.DialogsDirectory);
            Model.Scripts = WorkingManager.GetAllScriptNames();
            Model.CharacterClasses = WorkingManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory);
            CharacterClass charClass = new CharacterClass();
            charClass.Name = "(No Class)";
            Model.CharacterClasses.Insert(0, charClass);

            if (Model.Creatures.Count > 0)
            {
                Model.SelectedCreature = Model.Creatures[0];
            }
        }

        public void AnimationsModified(object sender, GameObjectListEventArgs e)
        {
            BindingList<SpriteAnimation> animations = new BindingList<SpriteAnimation>(e.GameObjects.Cast<SpriteAnimation>().ToList());
            Model.Animations = animations;
        }

        private void ClassSelected(object sender, SelectionChangedEventArgs e)
        {
            if (lbClass.SelectedItem != null)
            {
                Model.SelectedCreature.CharacterClassResref = (lbClass.SelectedItem as CharacterClass).Resref;
            }
        }

        private void DialogSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ddlDialog.SelectedItem != null)
            {
                Model.SelectedCreature.DialogResref = (ddlDialog.SelectedItem as Dialog).Resref;
            }
        }
    }
}
