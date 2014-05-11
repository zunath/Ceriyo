using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.ViewModels
{
    public class ScriptSelectionVM : BaseVM
    {
        private BindingList<string> _scripts;

        public BindingList<string> Scripts
        {
            get { return _scripts; }
            set
            {
                _scripts = value;
                OnPropertyChanged("Scripts");
            }
        }

        public ScriptSelectionVM()
        {
            this._scripts = new BindingList<string>();
        }
    }
}
