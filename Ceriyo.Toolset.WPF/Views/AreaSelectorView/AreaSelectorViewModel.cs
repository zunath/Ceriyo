using System.ComponentModel;
using Ceriyo.Core.Data;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.AreaSelectorView
{
    public class AreaSelectorViewModel : BindableBase
    {
        public AreaSelectorViewModel()
        {

        }


        private AreaData _selectedArea;

        public AreaData SelectedArea
        {
            get { return _selectedArea; }
            set { SetProperty(ref _selectedArea, value); }
        }

        private BindingList<AreaData> _areas;

        public BindingList<AreaData> Areas
        {
            get { return _areas; }
            set { SetProperty(ref _areas, value); }
        }


    }
}
