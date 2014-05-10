using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class LoadModuleVM : BaseVM
    {
        private BindingList<GameModule> _gameModules;

        public BindingList<GameModule> GameModules
        {
            get { return _gameModules; }
            set
            {
                _gameModules = value;
                OnPropertyChanged("GameModules");
            }
        }

        public LoadModuleVM()
        {
            this._gameModules = new BindingList<GameModule>();
        }
    }
}
