using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class AbilityEditorVM : BaseVM
    {
        private BindingList<Ability> _abilities;
        private Ability _selectedAbility;
        private bool _isAbilitySelected;
        private BindingList<string> _scripts; 

        public BindingList<Ability> Abilities
        {
            get
            {
                return _abilities;
            }
            set
            {
                _abilities = value;
                OnPropertyChanged("Abilities");
            }
        }

        public Ability SelectedAbility
        {
            get
            {
                return _selectedAbility;
            }
            set
            {
                _selectedAbility = value;
                OnPropertyChanged("SelectedAbility");
            }
        }

        public bool IsAbilitySelected
        {
            get
            {
                return _isAbilitySelected;
            }
            set
            {
                _isAbilitySelected = value;
                OnPropertyChanged("IsAbilitySelected");
            }
        }

        public BindingList<string> Scripts
        {
            get
            {
                return _scripts;
            }
            set
            {
                _scripts = value;
                OnPropertyChanged("Scripts");
            }
        }

        public AbilityEditorVM()
        {
            Abilities = new BindingList<Ability>();
            IsAbilitySelected = false;
            SelectedAbility = new Ability();
            Scripts = new BindingList<string>();
        }
    }
}
