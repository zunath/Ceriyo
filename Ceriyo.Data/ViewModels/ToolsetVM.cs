using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ToolsetVM : BaseVM
    {
        private GameModule _module;
        public GameModule Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
                OnPropertyChanged("Module");
            }
        }

        public ToolsetVM()
        {
            this.Module = new GameModule();
        }

    }
}
