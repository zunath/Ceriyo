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

                RefreshAnimationList();
            }
        }

        private void RefreshAnimationList()
        {
            if (Model.SelectedCreature != null)
            {
                LoadAnimation(AnimationTypeEnum.MoveEast, ddlMoveEastAnimation);
                LoadAnimation(AnimationTypeEnum.MoveWest, ddlMoveWestAnimation);
                LoadAnimation(AnimationTypeEnum.MoveNorth, ddlMoveNorthAnimation);
                LoadAnimation(AnimationTypeEnum.MoveSouth, ddlMoveSouthAnimation);
                LoadAnimation(AnimationTypeEnum.MoveSouthEast, ddlMoveSoutheastAnimation);
                LoadAnimation(AnimationTypeEnum.MoveSouthWest, ddlMoveSouthwestAnimation);
                LoadAnimation(AnimationTypeEnum.MoveNorthEast, ddlMoveNortheastAnimation);
                LoadAnimation(AnimationTypeEnum.MoveNorthWest, ddlMoveNorthwestAnimation);
            }
        }

        private void LoadAnimation(AnimationTypeEnum animationType, ComboBox box)
        {
            box.SelectedItem = string.IsNullOrWhiteSpace(Model.SelectedCreature.AnimationResrefs[animationType]) ?
                box.SelectedItem = Model.Animations[0] :
                Model.Animations.SingleOrDefault(x => x.Resref == Model.SelectedCreature.AnimationResrefs[animationType]);
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
            SpriteAnimation animation = new SpriteAnimation();
            animation.Name = "(No Animation)";
            Model.Animations.Insert(0, animation);

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

            RefreshAnimationList();
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

        private void MoveNorthAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveNorthAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveNorth] = animation.Resref;
            }
        }
        private void MoveSouthAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveSouthAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveSouth] = animation.Resref;
            }
        }
        private void MoveEastAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveEastAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveEast] = animation.Resref;
            }
        }
        private void MoveWestAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveWestAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveWest] = animation.Resref;
            }
        }
        private void MoveNorthwestAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveNorthwestAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveNorthWest] = animation.Resref;
            }
        }
        private void MoveNortheastAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveNortheastAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveNorthEast] = animation.Resref;
            }
        }
        private void MoveSouthwestAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveSouthwestAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveSouthWest] = animation.Resref;
            }
        }
        private void MoveSoutheastAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlMoveSoutheastAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.MoveSouthEast] = animation.Resref;
            }
        }
    }
}
