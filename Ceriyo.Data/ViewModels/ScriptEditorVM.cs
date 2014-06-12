using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ScriptEditorVM : BaseVM
    {
        private BindingList<string> _scriptNames;
        private BindingList<GameScript> _openScripts;

        public BindingList<string> ScriptNames
        {
            get
            {
                return _scriptNames;
            }
            set
            {
                _scriptNames = value;
                OnPropertyChanged("ScriptNames");
            }
        }

        public BindingList<GameScript> OpenScripts
        {
            get
            {
                return _openScripts;
            }
            set
            {
                _openScripts = value;
                OnPropertyChanged("OpenScripts");
            }
        }

        public ScriptEditorVM()
        {
            this.ScriptNames = new BindingList<string>();
            this.OpenScripts = new BindingList<GameScript>();
        }
    }
}
