using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
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
    public partial class CreatureEditorComponent
    {
        private CreatureEditorVM Model { get; set; }

        public CreatureEditorComponent()
        {
            InitializeComponent();
            Model = new CreatureEditorVM();
            DataContext = Model;
        }

        private void CreatureSelected(object sender, SelectionChangedEventArgs e)
        {
            Creature creature = lbCreatures.SelectedItem as Creature;
            Model.SelectedCreature = creature;
            Model.IsCreatureSelected = creature != null;

            if (creature == null) return;
            lbClass.SelectedItem = string.IsNullOrWhiteSpace(creature.CharacterClassResref) ? 
                lbClass.Items[0] : 
                Model.CharacterClasses.SingleOrDefault(x => x.Resref == creature.CharacterClassResref);

            RefreshAnimationList();
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

                LoadAnimation(AnimationTypeEnum.IdleEast, ddlIdleEastAnimation);
                LoadAnimation(AnimationTypeEnum.IdleNorth, ddlIdleNorthAnimation);
                LoadAnimation(AnimationTypeEnum.IdleSouth, ddlIdleSouthAnimation);
                LoadAnimation(AnimationTypeEnum.IdleWest, ddlIdleWestAnimation);
                LoadAnimation(AnimationTypeEnum.IdleNorthEast, ddlIdleNortheastAnimation);
                LoadAnimation(AnimationTypeEnum.IdleNorthWest, ddlIdleNorthwestAnimation);
                LoadAnimation(AnimationTypeEnum.IdleSouthEast, ddlIdleSoutheastAnimation);
                LoadAnimation(AnimationTypeEnum.IdleSouthWest, ddlIdleSouthwestAnimation);
            }
        }

        private void LoadAnimation(AnimationTypeEnum animationType, ComboBox box)
        {
            if (Model.SelectedCreature.AnimationResrefs.ContainsKey(animationType))
            {
                // Verify the animation currently set is actually valid. If not, remove it.
                SpriteAnimation animation =
                    Model.Animations.SingleOrDefault(
                        x => x.Resref == Model.SelectedCreature.AnimationResrefs[animationType]);
                if (animation == null)
                {
                    Model.SelectedCreature.AnimationResrefs[animationType] = string.Empty;
                }

                box.SelectedItem = string.IsNullOrWhiteSpace(Model.SelectedCreature.AnimationResrefs[animationType]) ?
                    box.SelectedItem = Model.Animations[0] :
                    Model.Animations.SingleOrDefault(x => x.Resref == Model.SelectedCreature.AnimationResrefs[animationType]);
            }
            else
            {
                Model.SelectedCreature.AnimationResrefs.Add(animationType, string.Empty);
                box.SelectedItem = Model.Animations[0];
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
            string resref = GameResourceProcessor.GenerateUniqueResref(Model.Creatures.Cast<IGameObject>().ToList(), creature.CategoryName);

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
            FileOperationResultTypeEnum result = WorkingDataManager.ReplaceAllGameObjectFiles(Model.Creatures.Cast<IGameObject>().ToList(), WorkingPaths.CreaturesDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save creatures.", "Saving creatures failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Animations = WorkingDataManager.GetAllGameObjects<SpriteAnimation>(ModulePaths.AnimationsDirectory);
            SpriteAnimation animation = new SpriteAnimation
            {
                Name = "(No Animation)"
            };
            Model.Animations.Insert(0, animation);

            Model.Creatures = WorkingDataManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
            Model.Dialogs = WorkingDataManager.GetAllGameObjects<Dialog>(ModulePaths.DialogsDirectory);
            Model.Scripts = WorkingDataManager.GetAllScriptNames();
            
            Model.CharacterClasses = WorkingDataManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory);
            CharacterClass charClass = new CharacterClass
            {
                Name = "(No Class)"
            };
            Model.CharacterClasses.Insert(0, charClass);

            if (Model.Creatures.Count > 0)
            {
                Model.SelectedCreature = Model.Creatures[0];
            }
        }

        public void AnimationsModified(object sender, EditorItemChangedEventArgs e)
        {
            if (e.IsAdded)
            {
                Model.Animations.Add(e.GameObject as SpriteAnimation);
            }
            else
            {
                SpriteAnimation animation = Model.Animations.SingleOrDefault(x => x.Resref == e.Resref);
                if (animation != null)
                {
                    Model.Animations.Remove(animation);   
                }
            }

            RefreshAnimationList();
        }

        public void ClassesModified(object sender, EditorItemChangedEventArgs e)
        {
            CharacterClass charClass = Model.CharacterClasses.SingleOrDefault(x => x.Resref == e.Resref);
            if (e.IsChanged)
            {
                if (charClass == null) return;
                int index = Model.CharacterClasses.IndexOf(charClass);
                Model.CharacterClasses[index] = e.GameObject as CharacterClass;
            }
            else if (e.IsAdded)
            {
                Model.CharacterClasses.Add(e.GameObject as CharacterClass);
            }
            else
            {
                if (charClass == null) return;
                Model.CharacterClasses.Remove(charClass);
            }
        }

        private void ClassSelected(object sender, SelectionChangedEventArgs e)
        {
            if (lbClass.SelectedItem == null) return;
            CharacterClass characterClass = lbClass.SelectedItem as CharacterClass;
            if (characterClass != null)
            {
                Model.SelectedCreature.CharacterClassResref = characterClass.Resref;
            }
        }

        private void DialogSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ddlDialog.SelectedItem == null) return;
            Dialog dialog = ddlDialog.SelectedItem as Dialog;
            if (dialog != null)
            {
                Model.SelectedCreature.DialogResref = dialog.Resref;
            }
        }

        #region Animation Hooking

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
        private void IdleNorthAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleNorthAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleNorth] = animation.Resref;
            }
        }
        private void IdleSouthAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleSouthAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleSouth] = animation.Resref;
            }
        }
        private void IdleEastAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleEastAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleEast] = animation.Resref;
            }
        }
        private void IdleWestAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleWestAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleWest] = animation.Resref;
            }
        }
        private void IdleNorthwestAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleNorthwestAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleNorthWest] = animation.Resref;
            }
        }
        private void IdleNortheastAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleNortheastAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleNorthEast] = animation.Resref;
            }
        }
        private void IdleSoutheastAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleSoutheastAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleSouthEast] = animation.Resref;
            }
        }
        private void IdleSouthwestAnimationChanged(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = ddlIdleSouthwestAnimation.SelectedItem as SpriteAnimation;
            if (animation != null)
            {
                Model.SelectedCreature.AnimationResrefs[AnimationTypeEnum.IdleSouthWest] = animation.Resref;
            }
        }

        #endregion
    }
}
