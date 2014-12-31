using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for SkillEditorComponent.xaml
    /// </summary>
    public partial class SkillEditorComponent
    {
        private SkillEditorVM Model { get; set; }

        public SkillEditorComponent()
        {
            InitializeComponent();
            Model = new SkillEditorVM();
            DataContext = Model;
        }

        private void SkillSelected(object sender, SelectionChangedEventArgs e)
        {
            Skill skill = lbSkills.SelectedItem as Skill;
            Model.SelectedSkill = skill;
            Model.IsSkillSelected = skill != null;

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedSkill != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this skill?", "Delete skill?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Skills.Remove(Model.SelectedSkill);
                    Model.SelectedSkill = null;
                    Model.IsSkillSelected = false;
                }
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Skill skill = new Skill();
            string resref = GameResourceProcessor.GenerateUniqueResref(Model.Skills.Cast<IGameObject>().ToList(), skill.CategoryName);

            skill.Name = resref;
            skill.Tag = resref;
            skill.Resref = resref;

            Model.Skills.Add(skill);
            int index = Model.Skills.IndexOf(skill);
            Model.SelectedSkill = Model.Skills[index];
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingDataManager.ReplaceAllGameObjectFiles(Model.Skills.Cast<IGameObject>().ToList(), WorkingPaths.SkillsDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save skills.", "Saving skills failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Skills = WorkingDataManager.GetAllGameObjects<Skill>(ModulePaths.SkillsDirectory);
            Model.Scripts = WorkingDataManager.GetAllScriptNames();

            if (Model.Skills.Count > 0)
            {
                Model.SelectedSkill = Model.Skills[0];
            }
        }
    }
}
