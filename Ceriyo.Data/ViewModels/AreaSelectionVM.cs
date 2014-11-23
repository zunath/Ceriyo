using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class AreaSelectionVM : BaseVM
    {
        private BindingList<Area> _areas;
        private bool _isAreaLoaded;
        private bool _isModuleLoaded;
        private Area _selectedArea;
        private bool _isAreaSelected;

        public Area SelectedArea
        {
            get
            {
                return _selectedArea;
            }
            set
            {
                _selectedArea = value;
                OnPropertyChanged("SelectedArea");
            }

        }

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

        public bool IsModuleLoaded
        {
            get { return _isModuleLoaded; }
            set
            {
                _isModuleLoaded = value;
                OnPropertyChanged("IsModuleLoaded");
            }
        }

        public bool IsAreaSelected
        {
            get { return _isAreaSelected; }
            set
            {
                _isAreaSelected = value;
                OnPropertyChanged("IsAreaSelected");
            }
        }

        public AreaSelectionVM()
        {
            Areas = new BindingList<Area>();
            IsAreaLoaded = false;
            IsModuleLoaded = false;
            IsAreaSelected = false;
            SelectedArea = new Area();
        }
    }
}
