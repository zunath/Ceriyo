using System;
using System.ComponentModel;
using System.IO;
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

            AreaDataObservable obs1 = _areaFactory.Invoke();
            obs1.Name = "area 1";
            AreaDataObservable obs2 = _areaFactory.Invoke();
            obs2.Name = "area 2";
            AreaDataObservable obs3 = _areaFactory.Invoke();
            obs3.Name = "area 3";
            AreaDataObservable obs4 = _areaFactory.Invoke();
            obs4.Name = "area 4";
            AreaDataObservable obs5 = _areaFactory.Invoke();
            obs5.Name = "area 5";

            Areas.Add(obs1);
            Areas.Add(obs2);
            Areas.Add(obs3);
            Areas.Add(obs4);
            Areas.Add(obs5);
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
            set { SetProperty(ref _selectedArea, value); }
        }

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

    }
}
