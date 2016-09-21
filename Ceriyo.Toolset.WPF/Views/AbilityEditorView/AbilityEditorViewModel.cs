using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Core.Data;
using Prism.Commands;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.AbilityEditorView
{
    public class AbilityEditorViewModel : BindableBase
    {
        public AbilityEditorViewModel()
        {

        }


        private BindingList<AbilityData> _abilities;

        public BindingList<AbilityData> Abilities
        {
            get { return _abilities; }
            set { SetProperty(ref _abilities, value); }
        }

        private AbilityData _selectedAbility;
        public AbilityData SelectedAbility
        {
            get { return _selectedAbility; }
            set
            {
                SetProperty(ref _selectedAbility, value);
                OnPropertyChanged("IsAbilitySelected");
            }
        }

        private Dictionary<string, ScriptData> _scripts;

        public Dictionary<string, ScriptData> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsAbilitySelected => SelectedAbility != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }


    }
}
