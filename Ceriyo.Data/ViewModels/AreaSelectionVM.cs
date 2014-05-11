﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class AreaSelectionVM : BaseVM
    {
        private BindingList<Area> _areas;
        private bool _isAreaLoaded;

        public BindingList<Area> Areas
        {
            get { return _areas; }
            set 
            { 
                _areas = value;
                OnPropertyChanged("Areas");
            }
        }

        public bool IsAreaLoaded
        {
            get { return _isAreaLoaded; }
            set
            {
                _isAreaLoaded = value;
                OnPropertyChanged("IsAreaLoaded");
            }
        }

        public AreaSelectionVM()
        {
            this._areas = new BindingList<Area>();
            this._isAreaLoaded = false;
        }
    }
}
