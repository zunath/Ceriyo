using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ResourceEditorVM : BaseVM
    {
        private BindingList<ResourceEditorItem> _resources;

        public BindingList<ResourceEditorItem> Resources 
        {
            get
            {
                return _resources;
            }
            set
            {
                _resources = value;
                OnPropertyChanged("Resources");
            }
        }

        public ResourceEditorVM()
        {
            this.Resources = new BindingList<ResourceEditorItem>();
        }
    }
}
