using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Ceriyo.Toolset.WPF.Events.Error;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.DetailedErrorView
{
    public class DetailedErrorViewModel : BindableBase, IInteractionRequestAware
    {
        private IEventAggregator _eventAggregator;

        public DetailedErrorViewModel()
        {

        }

        public DetailedErrorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            OkCommand = new DelegateCommand(Ok);

            _eventAggregator.GetEvent<DetailedErrorEvent>().Subscribe(DetailedError);
        }

        private void Ok()
        {
            FinishInteraction();
            HeaderMessage = string.Empty;
            DetailedErrorMessage = string.Empty;
        }

        private void DetailedError(Tuple<string, string> data)
        {
            HeaderMessage = data.Item1;
            DetailedErrorMessage = data.Item2;
        }

        private string _headerMessage;

        public string HeaderMessage
        {
            get { return _headerMessage; }
            set { SetProperty(ref _headerMessage, value); }
        }

        private string _detailedErrorMessage;

        public string DetailedErrorMessage
        {
            get { return _detailedErrorMessage; }
            set { SetProperty(ref _detailedErrorMessage, value); }
        }

        public DelegateCommand OkCommand { get; set; }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
