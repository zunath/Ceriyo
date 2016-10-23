using System;
using System.IO;
using System.Linq;
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

namespace Ceriyo.Toolset.WPF.Views.AreaPropertiesView
{
    public class AreaPropertiesViewModel : ValidatableBindableBase<AreaPropertiesViewModelValidator>, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IAreaDomainService _areaDomainService;
        private readonly IObjectMapper _objectMapper;
        private readonly IPathService _pathService;
        private readonly IDataService _dataService;

        public AreaPropertiesViewModel(
            IEventAggregator eventAggregator,
            IObservableDataFactory observableDataFactory,
            IAreaDomainService areaDomainService,
            IObjectMapper objectMapper,
            IPathService pathService,
            IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _observableDataFactory = observableDataFactory;
            _areaDomainService = areaDomainService;
            _objectMapper = objectMapper;
            _pathService = pathService;
            _dataService = dataService;

            Tilesets = new ObservableCollectionEx<TilesetDataObservable>();
            LocalVariables = new LocalVariableDataObservable();

            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            AddLocalDoubleCommand = new DelegateCommand(AddLocalDouble);
            AddLocalStringCommand = new DelegateCommand(AddLocalString);
            DeleteLocalDoubleCommand = new DelegateCommand<LocalDoubleDataObservable>(DeleteLocalDouble);
            DeleteLocalStringCommand = new DelegateCommand<LocalStringDataObservable>(DeleteLocalString);

            _eventAggregator.GetEvent<AreaPropertiesOpenedEvent>().Subscribe(AreaPropertiesOpened);
            _eventAggregator.GetEvent<ModuleOpenedEvent>().Subscribe(ModuleOpened);
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);
        }

        private void DataEditorClosed(bool hasSaved)
        {
            if (hasSaved)
            {
                LoadTilesets();
            }
        }

        private void ModuleOpened(string moduleName)
        {
            LoadTilesets();
        }

        private AreaDataObservable _editingArea;

        private void AreaPropertiesOpened(AreaDataObservable area)
        {
            _editingArea = area;
            Name = area.Name;
            Tag = area.Tag;
            Resref = area.Resref;
            Width = area.Width;
            Height = area.Height;

            SelectedTileset = Tilesets.SingleOrDefault(x => x.GlobalID == area.TilesetGlobalID);
            LocalVariables.Clear();

            foreach (var @string in area.LocalVariables.LocalStrings)
            {
                LocalVariables.LocalStrings.Add(@string);
            }

            foreach (var @double in area.LocalVariables.LocalDoubles)
            {
                LocalVariables.LocalDoubles.Add(@double);
            }
        }
        
        private void ClearForm()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Width = 1;
            Height = 1;
            LocalVariables.Clear();
            SelectedTileset = null;
            _editingArea = null;
            OnAreaHeartbeat = string.Empty;
            OnAreaEnter = string.Empty;
            OnAreaExit = string.Empty;
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

        private string _onAreaEnter;

        public string OnAreaEnter
        {
            get { return _onAreaEnter; }
            set { SetProperty(ref _onAreaEnter, value); }
        }

        private string _onAreaExit;

        public string OnAreaExit
        {
            get { return _onAreaExit; }
            set { SetProperty(ref _onAreaExit, value); }
        }

        private string _onAreaHeartbeat;

        public string OnAreaHeartbeat
        {
            get { return _onAreaHeartbeat; }
            set { SetProperty(ref _onAreaHeartbeat, value); }
        }

        private LocalVariableDataObservable _localVariables;

        public LocalVariableDataObservable LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _comments;

        public string Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        private ObservableCollectionEx<ScriptDataObservable> _scripts;

        public ObservableCollectionEx<ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        public DelegateCommand AddLocalDoubleCommand { get; }

        private void AddLocalDouble()
        {
            LocalVariables.LocalDoubles.Add(_observableDataFactory.Create<LocalDoubleDataObservable>());
        }

        public DelegateCommand<LocalDoubleDataObservable> DeleteLocalDoubleCommand { get; }

        private void DeleteLocalDouble(LocalDoubleDataObservable localDouble)
        {
            LocalVariables.LocalDoubles.Remove(localDouble);
        }

        public DelegateCommand AddLocalStringCommand { get; }

        private void AddLocalString()
        {
            LocalVariables.LocalStrings.Add(_observableDataFactory.Create<LocalStringDataObservable>());
        }

        public DelegateCommand<LocalStringDataObservable> DeleteLocalStringCommand { get; }

        private void DeleteLocalString(LocalStringDataObservable localString)
        {
            LocalVariables.LocalStrings.Remove(localString);
        }

        public DelegateCommand SaveCommand { get; }

        private void Save()
        {
            _editingArea.Name = Name;
            _editingArea.Tag = Tag;
            _editingArea.Resref = Resref;
            _editingArea.TilesetGlobalID = SelectedTileset.GlobalID;
            _editingArea.Width = Width;
            _editingArea.Height = Height;
            _editingArea.OnAreaEnter = OnAreaEnter;
            _editingArea.OnAreaExit = OnAreaExit;
            _editingArea.OnAreaHeartbeat = OnAreaHeartbeat;
            _editingArea.Description = Description;
            _editingArea.Comments = Comments;

            _editingArea.LocalVariables.Clear();
            foreach (var @string in LocalVariables.LocalStrings)
            {
                _editingArea.LocalVariables.LocalStrings.Add(@string);
            }
            foreach (var @double in LocalVariables.LocalDoubles)
            {
                _editingArea.LocalVariables.LocalDoubles.Add(@double);
            }

            _areaDomainService.SaveArea(_objectMapper.Map<AreaData>(_editingArea));
            _eventAggregator.GetEvent<AreaPropertiesChangedEvent>().Publish(_editingArea);
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
