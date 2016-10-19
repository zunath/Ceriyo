using System;
using System.ComponentModel;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.NewModuleView
{
    public class NewModuleViewModel : ValidatableBindableBase<NewModuleViewModel>, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        
        public NewModuleViewModel(
            IObjectMapper objectMapper,
            NewModuleViewModelValidator validator,
            IEventAggregator eventAggregator)
            :base(objectMapper, validator)
        {
            _eventAggregator = eventAggregator;
            CreateModuleCommand = new DelegateCommand(CreateModule, CanCreateModule);
            CancelCommand = new DelegateCommand(Cancel);

            PropertyChanged += OnPropertyChanged;
        }

        private bool CanCreateModule()
        {
            return !HasErrors;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            CreateModuleCommand.RaiseCanExecuteChanged();
        }
        
        public DelegateCommand CreateModuleCommand { get; set; }
        private void CreateModule()
        {
            if (HasErrors) return;

            _eventAggregator.GetEvent<ModuleClosedEvent>().Publish();
            _eventAggregator.GetEvent<ModuleCreatedEvent>().Publish(new ModuleEventArgs(Name, Tag, Resref));
            FinishInteraction();
            _eventAggregator.GetEvent<ModuleLoadedEvent>().Publish(null);
            ClearFields();
        }

        public DelegateCommand CancelCommand { get; set; }
        private void Cancel()
        {
            FinishInteraction();
            ClearFields();
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
