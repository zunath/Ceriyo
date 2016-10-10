using System.ComponentModel;
using Ceriyo.Core.Data;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.DialogSelectorView
{
    public class DialogSelectorViewModel : BindableBase
    {
        public DialogSelectorViewModel()
        {

        }

        private DialogData _selectedDialog;

        public DialogData SelectedDialog
        {
            get { return _selectedDialog; }
            set { SetProperty(ref _selectedDialog, value); }
        }

        private BindingList<DialogData> _dialogs;

        public BindingList<DialogData> Dialogs
        {
            get { return _dialogs; }
            set { SetProperty(ref _dialogs, value); }
        }

    }
}
