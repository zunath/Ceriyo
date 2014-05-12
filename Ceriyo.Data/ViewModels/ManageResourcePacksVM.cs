using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.ViewModels
{
    public class ManageResourcePacksVM : BaseVM
    {
        private BindingList<string> _availableResourcePackages;
        private BindingList<string> _attachedResourcePackages;

        public BindingList<string> AvailableResourcePackages
        {
            get
            {
                return _availableResourcePackages;
            }
            set
            {
                _availableResourcePackages = value;
                OnPropertyChanged("AvailableResourcePackages");
            }
        }
        public BindingList<string> AttachedResourcePackages
        {
            get
            {
                return _attachedResourcePackages;
            }
            set
            {
                _attachedResourcePackages = value;
                OnPropertyChanged("AttachedResourcePackages");
            }
        }

        public ManageResourcePacksVM()
        {
            this.AttachedResourcePackages = new BindingList<string>();
            this.AvailableResourcePackages = new BindingList<string>();
        }
    }
}
