﻿using System;
using System.ComponentModel;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Observables;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.NewAreaView
{
    public class NewAreaViewModel : ValidatableBindableBase<NewAreaViewModel>, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleResourceDomainService _moduleResourceDomainService;
        private readonly IDataService _dataService;

        public NewAreaViewModel(
            IEventAggregator eventAggregator,
            IObjectMapper mapper,
            IModuleResourceDomainService moduleResourceDomainService,
            NewAreaViewModelValidator validator)
            : base(mapper, validator)
        {
            _eventAggregator = eventAggregator;
            _moduleResourceDomainService = moduleResourceDomainService;

            Tilesets = new ObservableCollectionEx<TilesetDataObservable>();
            OpenInAreaViewer = true;
            Width = 8;
            Height = 8;
            

            CreateAreaCommand = new DelegateCommand(CreateArea, CanCreateArea);
            CancelCommand = new DelegateCommand(Cancel);

            PropertyChanged += OnPropertyChanged;
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

        }

        public DelegateCommand CancelCommand { get; }

        private void Cancel()
        {
            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
