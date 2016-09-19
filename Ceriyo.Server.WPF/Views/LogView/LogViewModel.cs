using System.ComponentModel;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.LogView
{
    public class LogViewModel : BindableBase
    {
        public LogViewModel()
        {

        }

        private BindingList<string> _logMessages;

        public BindingList<string> LogMessages
        {
            get { return _logMessages; }
            set { SetProperty(ref _logMessages, value); }
        }

    }
}
