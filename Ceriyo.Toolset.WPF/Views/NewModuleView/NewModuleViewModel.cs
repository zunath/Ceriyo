using System;
using System.Windows.Input;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.DataServices;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.NewModuleView
{
    public class NewModuleViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;

        public NewModuleViewModel()
        {
        }

        public NewModuleViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CreateModuleCommand = new DelegateCommand(CreateModule);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }
        private string _resref;

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        public DelegateCommand CreateModuleCommand { get; set; }
        private void CreateModule()
        {
            _eventAggregator.GetEvent<ModuleCreatedEvent>().Publish(new ModuleEventArgs(Name, Tag, Resref));
            FinishInteraction();
            _eventAggregator.GetEvent<ModuleLoadedEvent>().Publish();
            ClearFields();
        }

        public DelegateCommand CancelCommand { get; set; }
        private void Cancel()
        {
            FinishInteraction();
            ClearFields();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
