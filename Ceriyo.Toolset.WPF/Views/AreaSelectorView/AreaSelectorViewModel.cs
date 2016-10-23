using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Area;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.AreaSelectorView
{
    public class AreaSelectorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPathService _pathService;
        private readonly IDataService _dataService;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IAreaDomainService _areaDomainService;
        private readonly IObjectMapper _objectMapper;

        public AreaSelectorViewModel(IEventAggregator eventAggregator,
            IPathService pathService,
            IDataService dataService,
            IObservableDataFactory observableDataFactory,
            IAreaDomainService areaDomainService,
            IObjectMapper objectMapper)
        {
            _eventAggregator = eventAggregator;
            _pathService = pathService;
            _dataService = dataService;
            _observableDataFactory = observableDataFactory;
            _areaDomainService = areaDomainService;
            _objectMapper = objectMapper;

            Areas = new ObservableCollectionEx<AreaDataObservable>();
            
            CreateAreaCommand = new DelegateCommand(CreateArea);
            DeleteAreaCommand = new DelegateCommand(DeleteArea);
            OpenAreaPropertiesCommand = new DelegateCommand(OpenAreaProperties);
            CreateAreaRequest = new InteractionRequest<INotification>();
            OpenAreaPropertiesRequest = new InteractionRequest<INotification>();
            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();

            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
            _eventAggregator.GetEvent<AreaCreatedEvent>().Subscribe(AreaCreated);
        }

        private void AreaCreated(AreaDataObservable area)
        {
            Areas.Add(area);
            IsAreaListExpanded = true;
        }

        private void ModuleClosed()
        {
            Areas.Clear();
            IsModuleLoaded = false;
        }

        private void ModuleLoaded(string s)
        {
            LoadExistingData();
            IsModuleLoaded = true;
        }

        private void LoadExistingData()
        {
            Areas.Clear();
            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Area/", "*.dat");

            foreach (var file in files)
            {
                AreaData loaded = _dataService.Load<AreaData>(file);
                AreaDataObservable area = _observableDataFactory.CreateAndMap<AreaDataObservable, AreaData>(loaded);
                Areas.Add(area);
            }
        }

        private bool _isModuleLoaded;

        public bool IsModuleLoaded
        {
            get { return _isModuleLoaded; }
            set { SetProperty(ref _isModuleLoaded, value); }
        }

        private object _selectedTreeItem;

        public object SelectedTreeItem
        {
            get { return _selectedTreeItem; }
            set
            {
                SetProperty(ref _selectedTreeItem, value);
                OnPropertyChanged(nameof(IsAreaSelected));
            }
        }

        public AreaDataObservable SelectedArea
        {
            get
            {
                if (SelectedTreeItem.GetType() == typeof(AreaDataObservable))
                {
                    return (AreaDataObservable) SelectedTreeItem;
                }

                return null;
            }
        }

        public bool IsAreaSelected => SelectedArea != null;

        private ObservableCollectionEx<AreaDataObservable> _areas;

        public ObservableCollectionEx<AreaDataObservable> Areas
        {
            get { return _areas; }
            set { SetProperty(ref _areas, value); }
        }

        public DelegateCommand CreateAreaCommand { get; }
        public InteractionRequest<INotification> CreateAreaRequest { get; }

        private void CreateArea()
        {
            CreateAreaRequest.Raise(new Notification
            {
                Content = "Create Area",
                Title = "Create Area"
            });
        }
        
        public DelegateCommand OpenAreaPropertiesCommand { get; }
        public InteractionRequest<INotification> OpenAreaPropertiesRequest { get; }

        private void OpenAreaProperties()
        {
            _eventAggregator.GetEvent<AreaPropertiesOpenedEvent>().Publish(SelectedArea);
            OpenAreaPropertiesRequest.Raise(new Notification
            {
                Content = "Area Properties",
                Title = "Area Properties"
            });
        }

        public DelegateCommand DeleteAreaCommand { get; }
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

        private void DeleteArea()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Area?",
                    Content = "Are you sure you want to delete this area?"
                }, c =>
                {
                    if (!c.Confirmed) return;

                    AreaData domainObject = _objectMapper.Map<AreaData>(SelectedArea);
                    _areaDomainService.DeleteArea(domainObject);
                    Areas.Remove(SelectedArea);
                    _eventAggregator.GetEvent<AreaDeletedEvent>().Publish(SelectedArea);
                    
                });
        }

        private bool _isAreaListExpanded;

        public bool IsAreaListExpanded
        {
            get { return _isAreaListExpanded; }
            set { SetProperty(ref _isAreaListExpanded, value); }
        }

    }
}
