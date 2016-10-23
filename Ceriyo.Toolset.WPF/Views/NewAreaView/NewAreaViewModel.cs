using System;
using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Area;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.NewAreaView
{
    public class NewAreaViewModel : ValidatableBindableBase<NewAreaViewModelValidator>, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IPathService _pathService;
        private readonly IAreaDomainService _areaDomainService;
        private readonly IObjectMapper _objectMapper;

        public NewAreaViewModel(
            IEventAggregator eventAggregator,
            IObservableDataFactory observableDataFactory,
            IPathService pathService,
            IDataService dataService,
            IAreaDomainService areaDomainService,
            IObjectMapper objectMapper)
        {
            _eventAggregator = eventAggregator;
            _observableDataFactory = observableDataFactory;
            _pathService = pathService;
            _dataService = dataService;
            _areaDomainService = areaDomainService;
            _objectMapper = objectMapper;

            Tilesets = new ObservableCollectionEx<TilesetDataObservable>();
            OpenInAreaViewer = true;
            Width = 8;
            Height = 8;
            

            CreateAreaCommand = new DelegateCommand(CreateArea, CanCreateArea);
            CancelCommand = new DelegateCommand(Cancel);

            PropertyChanged += OnPropertyChanged;
            
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);
            _eventAggregator.GetEvent<ModuleOpenedEvent>().Subscribe(ModuleOpened);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
        }

        private void ModuleOpened(string moduleName)
        {
            LoadTilesets();
        }

        private void ModuleClosed()
        {
            Tilesets.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadTilesets();
        }

        private void ClearForm()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            SelectedTileset = null;
            Width = 8;
            Height = 8;
            LaunchAreaProperties = false;
            OpenInAreaViewer = true;

        }

        private void LoadTilesets()
        {
            Tilesets.Clear();
            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Tileset/", "*.dat");

            foreach (var file in files)
            {
                var loaded = _dataService.Load<TilesetData>(file);
                var tileset = _observableDataFactory.CreateAndMap<TilesetDataObservable, TilesetData>(loaded);
                Tilesets.Add(tileset);
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CreateAreaCommand.RaiseCanExecuteChanged();
        }

        private bool CanCreateArea()
        {
            return !HasErrors;
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

        private ObservableCollectionEx<TilesetDataObservable> _tilesets;

        public ObservableCollectionEx<TilesetDataObservable> Tilesets
        {
            get { return _tilesets; }
            set { SetProperty(ref _tilesets, value); }
        }

        private TilesetDataObservable _selectedTileset;

        public TilesetDataObservable SelectedTileset
        {
            get { return _selectedTileset; }
            set { SetProperty(ref _selectedTileset, value); }
        }

        private int _width;

        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        private int _height;

        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        private bool _launchAreaProperties;

        public bool LaunchAreaProperties
        {
            get { return _launchAreaProperties; }
            set { SetProperty(ref _launchAreaProperties, value); }
        }

        private bool _openInAreaViewer;

        public bool OpenInAreaViewer
        {
            get { return _openInAreaViewer; }
            set { SetProperty(ref _openInAreaViewer, value); }
        }

        public DelegateCommand CreateAreaCommand { get; }

        private void CreateArea()
        {
            if (HasErrors) return;

            var area = _observableDataFactory.Create<AreaDataObservable>();
            area.Name = Name;
            area.Tag = Tag;
            area.Resref = Resref;
            area.Width = Width;
            area.Height = Height;
            area.TilesetGlobalID = SelectedTileset.GlobalID;

            var areaDomain = _objectMapper.Map<AreaData>(area);
            _areaDomainService.SaveArea(areaDomain);

            _eventAggregator.GetEvent<AreaCreatedEvent>().Publish(area);

            Notification.Content = new Tuple<bool, bool>(LaunchAreaProperties, OpenInAreaViewer);
            FinishInteraction();
            ClearForm();
        }

        public DelegateCommand CancelCommand { get; }

        private void Cancel()
        {
            FinishInteraction();
            ClearForm();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
