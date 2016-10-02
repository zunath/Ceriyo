using System;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.DataEditorView
{
    public class DataEditorViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;

        public DataEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public DelegateCommand OkCommand { get; set; }

        private void Ok()
        {
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Publish(true);
            FinishInteraction();
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Publish(false);
            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
