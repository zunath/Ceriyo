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

        private BindingList<string> _areaFileList;
        public BindingList<string> AreaFileList
        {
            get
            {
                return _areaFileList;
            }
            set
            {
                _areaFileList = value;
                OnPropertyChanged("AreaFileList");
            }
        }

        public ToolsetVM()
        {
            this.Module = new GameModule();
            this.AreaFileList = new BindingList<string>();
        }

    }
}
