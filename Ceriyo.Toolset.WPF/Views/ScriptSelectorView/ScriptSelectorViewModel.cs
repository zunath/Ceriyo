using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Events.Script;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ScriptSelectorView
{
    public class ScriptSelectorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDataService _moduleDataService;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IDataDomainService _dataDomainService;
        private readonly IObjectMapper _objectMapper;

        public ScriptSelectorViewModel(
            IEventAggregator eventAggregator,
            IModuleDataService moduleDataService,
            IObservableDataFactory observableDataFactory,
            IDataDomainService dataDomainService,
            IObjectMapper objectMapper)
        {
            _eventAggregator = eventAggregator;
            _moduleDataService = moduleDataService;
            _observableDataFactory = observableDataFactory;
            _dataDomainService = dataDomainService;
            _objectMapper = objectMapper;

            CreateScriptCommand = new DelegateCommand(CreateScript);
            DeleteScriptCommand = new DelegateCommand(DeleteScript);
            EditScriptCommand = new DelegateCommand(EditScript);
            
            CreateScriptRequest = new InteractionRequest<INotification>();
            EditScriptRequest = new InteractionRequest<INotification>();
            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();


            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
            _eventAggregator.GetEvent<ScriptCreatedEvent>().Subscribe(ScriptCreated);

            Scripts = new ObservableCollectionEx<ScriptDataObservable>();
        }
        
        private bool _isScriptListExpanded;

        public bool IsScriptListExpanded
        {
            get => _isScriptListExpanded;
            set => SetProperty(ref _isScriptListExpanded, value);
        }

        private bool _isModuleLoaded;

        public bool IsModuleLoaded
        {
            get => _isModuleLoaded;
            set => SetProperty(ref _isModuleLoaded, value);
        }

        private void ScriptCreated(ScriptDataObservable script)
        {
            Scripts.Add(script);
            IsScriptListExpanded = true;
        }

        private void ModuleClosed()
        {
            Scripts.Clear();
            IsModuleLoaded = false;
            IsScriptListExpanded = false;
        }

        private void ModuleLoaded(string s)
        {
            LoadExistingData();
            IsModuleLoaded = true;
            IsScriptListExpanded = true;
        }

        private void LoadExistingData()
        {
            Scripts.Clear();
            foreach (var loaded in _moduleDataService.LoadAll<ScriptData>())
            {
                ScriptDataObservable script = _observableDataFactory.CreateAndMap<ScriptDataObservable, ScriptData>(loaded);
                Scripts.Add(script);
            }
        }

        private ScriptDataObservable _selectedScript;

        public ScriptDataObservable SelectedScript
        {
            get => _selectedScript;
            set => SetProperty(ref _selectedScript, value);
        }

        private ObservableCollectionEx<ScriptDataObservable> _scripts;

        public ObservableCollectionEx<ScriptDataObservable> Scripts
        {
            get => _scripts;
            set => SetProperty(ref _scripts, value);
        }

        public DelegateCommand CreateScriptCommand { get; }
        public InteractionRequest<INotification> CreateScriptRequest;

        private void CreateScript()
        {
            
        }

        public DelegateCommand EditScriptCommand { get; }
        public InteractionRequest<INotification> EditScriptRequest;

        private void EditScript()
        {
            
        }

        public DelegateCommand DeleteScriptCommand { get; }
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }


        private void DeleteScript()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Script?",
                    Content = "Are you sure you want to delete this script?"
                }, c =>
                {
                    if (!c.Confirmed) return;

                    ScriptData domainObject = _objectMapper.Map<ScriptData>(SelectedScript);
                    _dataDomainService.DeleteData(domainObject);
                    Scripts.Remove(SelectedScript);
                    _eventAggregator.GetEvent<ScriptDeletedEvent>().Publish(SelectedScript);

                });
        }

        public bool IsScriptSelected => SelectedScript != null;

        private object _selectedTreeItem;

        public object SelectedTreeItem
        {
            get => _selectedTreeItem;
            set
            {
                SetProperty(ref _selectedTreeItem, value);
                RaisePropertyChanged(nameof(IsScriptSelected));
            }
        }
    }
}
