using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class AreaSelectionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BindingList<Area> _areas;

        public BindingList<Area> Areas
        {
            get { return _areas; }
            set 
            { 
                _areas = value;
                OnPropertyChanged("Areas");
            }
        }

        public AreaSelectionVM()
        {
            this._areas = new BindingList<Area>();
        }


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
