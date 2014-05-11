using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class DialogSelectionVM : BaseVM
    {
        private BindingList<Dialog> _dialogs;

        public BindingList<Dialog> Dialogs
        {
            get { return _dialogs; }
            set
            {
                _dialogs = value;
                OnPropertyChanged("Dialogs");
            }
        }

        public DialogSelectionVM()
        {
            this._dialogs = new BindingList<Dialog>();
        }

    }
}
