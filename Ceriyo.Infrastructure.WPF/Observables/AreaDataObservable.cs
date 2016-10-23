using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class AreaDataObservable: ValidatableBindableBase<AreaDataObservableValidator>, IDataObservable
    {
        private string _globalID;
        private string _name;
        private string _tag;
        private string _resref;
        private string _comments;
        private string _description;
        private string _onAreaEnter;
        private string _onAreaExit;
        private string _onAreaHeartbeat;
        private int _width;
        private int _height;
        private string _tilesetGlobalID;
        private bool _isOpenedInAreaEditor;

        private LocalVariableDataObservable _localVariables;
        

        public string OnAreaHeartbeat
        {
            get { return _onAreaHeartbeat; }
            set { SetProperty(ref _onAreaHeartbeat, value); }
        }

        public string OnAreaExit
        {
            get { return _onAreaExit; }
            set { SetProperty(ref _onAreaExit, value); }
        }

        public string OnAreaEnter
        {
            get { return _onAreaEnter; }
            set { SetProperty(ref _onAreaEnter, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }

        public LocalVariableDataObservable LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
        }

        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        public string TilesetGlobalID
        {
            get { return _tilesetGlobalID; }
            set { SetProperty(ref _tilesetGlobalID, value); }
        }

        public bool IsOpenedInAreaEditor
        {
            get { return _isOpenedInAreaEditor; }
            set { SetProperty(ref _isOpenedInAreaEditor, value); }
        }

        public AreaDataObservable()
        {
            GlobalID = Guid.NewGuid().ToString();
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            OnAreaEnter = string.Empty;
            OnAreaExit = string.Empty;
            OnAreaHeartbeat = string.Empty;
            LocalVariables = new LocalVariableDataObservable();
            Width = 8;
            Height = 8;
            TilesetGlobalID = string.Empty;

            LocalVariables.VariablesPropertyChanged += (sender, args) => OnPropertyChanged();
            LocalVariables.VariablesCollectionChanged += (sender, args) => OnPropertyChanged();
            LocalVariables.VariablesItemPropertyChanged += (sender, args) => OnPropertyChanged();
        }
    }
}
