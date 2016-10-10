using System.ComponentModel;
using Ceriyo.Core.Data;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ScriptSelectorView
{
    public class ScriptSelectorViewModel : BindableBase
    {
        public ScriptSelectorViewModel()
        {

        }

        private ScriptData _selectedScript;

        public ScriptData SelectedScript
        {
            get { return _selectedScript; }
            set { SetProperty(ref _selectedScript, value); }
        }

        private BindingList<ScriptData> _scripts;

        public BindingList<ScriptData> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

    }
}
