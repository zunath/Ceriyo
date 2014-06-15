using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.ViewModels
{
    public class AreaToolbarVM : BaseVM
    {
        private int _activeLayer;

        public int ActiveLayer
        {
            get
            {
                return _activeLayer;
            }
            set
            {
                _activeLayer = value;
                OnPropertyChanged("ActiveLayer");
            }
        }

        public AreaToolbarVM()
        {
            this.ActiveLayer = 0;
        }
    }
}
