using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class AbilityEditorVM : BaseVM
    {
        private BindingList<Ability> _abilities;
        private Ability _selectedAbility;
        private bool _isAbilitySelected;

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

        public AbilityEditorVM()
        {
            this.Abilities = new BindingList<Ability>();
            this.IsAbilitySelected = false;
            this.SelectedAbility = new Ability();
        }
    }
}
