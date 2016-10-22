using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.AreaSelectorView
{
    public class AreaSelectorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPathService _pathService;
        private readonly IDataService _dataService;
        private readonly AreaDataObservable.Factory _areaFactory;

        public AreaSelectorViewModel(IEventAggregator eventAggregator,
            IPathService pathService,
            IDataService dataService,
            AreaDataObservable.Factory areaFactory)
        {
            _eventAggregator = eventAggregator;
            _pathService = pathService;
            _dataService = dataService;
            _areaFactory = areaFactory;

            Areas = new ObservableCollectionEx<AreaDataObservable>();
            RootContextMenuItems = new List<MenuItem>();
            AreaContextMenuItems = new List<MenuItem>();

            CreateAreaCommand = new DelegateCommand(CreateArea);

            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
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
                AreaDataObservable area = _areaFactory.Invoke(loaded);
                Areas.Add(area);
            }
        }

        private bool _isModuleLoaded;

        public bool IsModuleLoaded
        {
            get { return _isModuleLoaded; }
            set { SetProperty(ref _isModuleLoaded, value); }
        }

        private AreaDataObservable _selectedArea;

        public AreaDataObservable SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                SetProperty(ref _selectedArea, value);
                OnPropertyChanged();
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

        private void CreateArea()
        {
            
        }

        public List<MenuItem> RootContextMenuItems { get; set; }
        public List<MenuItem> AreaContextMenuItems { get; set; }

        private void BuildContextMenuItems()
        {
            RootContextMenuItems.Add(new MenuItem
            {
                Header = "New Area",
                Command = new DelegateCommand(NewArea)
            });

            AreaContextMenuItems.Add(new MenuItem
            {
                Header = "Rename",
                Command = new DelegateCommand(RenameArea)
            });

            AreaContextMenuItems.Add(new MenuItem
            {
                Header = "Properties",
                Command = new DelegateCommand(OpenAreaProperties)
            });

            AreaContextMenuItems.Add(new MenuItem
            {
                Header = "Delete",
                Command = new DelegateCommand(DeleteArea)
            });

        }

        private void NewArea()
        {
            
        }

        private void RenameArea()
        {
            
        }

        private void OpenAreaProperties()
        {
            
        }

        private void DeleteArea()
        {
            
        }

    }
}
