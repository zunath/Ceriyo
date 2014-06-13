using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.ViewModels
{
    public class NewModuleVM : BaseVM
    {
        private string _name;
        private string _tag;
        private string _resref;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
                OnPropertyChanged("Tag");
            }
        }

        public string Resref
        {
            get
            {
                return _resref;
            }
            set
            {
                _resref = value;
                OnPropertyChanged("Resref");
            }
        }


        public NewModuleVM()
        {
            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Resref = string.Empty;
        }

    }
}
