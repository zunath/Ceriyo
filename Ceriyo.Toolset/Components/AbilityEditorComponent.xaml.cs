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
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AbilityEditorComponent.xaml
    /// </summary>
    public partial class AbilityEditorComponent : UserControl
    {
        private AbilityEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public AbilityEditorComponent()
        {
            InitializeComponent();
            this.Model = new AbilityEditorVM();
            this.Processor = new GameResourceProcessor();
            this.WorkingManager = new WorkingDataManager();
            this.DataContext = Model;
        }

        private void AbilitySelected(object sender, SelectionChangedEventArgs e)
        {
            Ability ability = lbAbilities.SelectedItem as Ability;
            Model.SelectedAbility = ability;
            Model.IsAbilitySelected = ability == null ? false : true;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedAbility != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this ability?", "Delete ability?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Abilities.Remove(Model.SelectedAbility);
                    Model.SelectedAbility = null;
                    Model.IsAbilitySelected = false;
                }
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Ability ability = new Ability();
            string resref = Processor.GenerateUniqueResref(Model.Abilities.Cast<IGameObject>().ToList(), ability.CategoryName);

            ability.Name = resref;
            ability.Tag = resref;
            ability.Resref = resref;

            Model.Abilities.Add(ability);
            int index = Model.Abilities.IndexOf(ability);
            Model.SelectedAbility = Model.Abilities[index];
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Abilities.Cast<IGameObject>().ToList(), WorkingPaths.AbilitiesDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save abilities.", "Saving abilities failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Abilities = WorkingManager.GetAllGameObjects<Ability>(ModulePaths.AbilitiesDirectory);


            if (Model.Abilities.Count > 0)
            {
                Model.SelectedAbility = Model.Abilities[0];
            }
        }
    }
}
