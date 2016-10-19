using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class PlaceableDataObservable: ValidatableBindableBase<PlaceableData>
    {
        public delegate PlaceableDataObservable Factory(PlaceableData data = null);

        private LocalVariableDataObservable _localVariables;
        private string _onUsed;
        private string _onAttacked;
        private string _onUnlocked;
        private string _onLocked;
        private string _onDisturbed;
        private string _onHeartbeat;
        private string _onDeath;
        private string _onDamaged;
        private string _onOpened;
        private string _onClosed;
        private string _keyTag;
        private bool _autoRemoveKey;
        private bool _isKeyRequired;
        private bool _isLocked;
        private bool _isUseable;
        private bool _isPlot;
        private bool _isStatic;
        private string _comment;
        private string _description;
        private string _resref;
        private string _tag;
        private string _globalID;
        private string _name;

        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        public bool IsStatic
        {
            get { return _isStatic; }
            set { SetProperty(ref _isStatic, value); }
        }

        public bool IsPlot
        {
            get { return _isPlot; }
            set { SetProperty(ref _isPlot, value); }
        }

        public bool IsUseable
        {
            get { return _isUseable; }
            set { SetProperty(ref _isUseable, value); }
        }

        public bool IsLocked
        {
            get { return _isLocked; }
            set { SetProperty(ref _isLocked, value); }
        }

        public bool IsKeyRequired
        {
            get { return _isKeyRequired; }
            set { SetProperty(ref _isKeyRequired, value); }
        }

        public bool AutoRemoveKey
        {
            get { return _autoRemoveKey; }
            set { SetProperty(ref _autoRemoveKey, value); }
        }

        public string KeyTag
        {
            get { return _keyTag; }
            set { SetProperty(ref _keyTag, value); }
        }

        public string OnClosed
        {
            get { return _onClosed; }
            set { SetProperty(ref _onClosed, value); }
        }

        public string OnOpened
        {
            get { return _onOpened; }
            set { SetProperty(ref _onOpened, value); }
        }

        public string OnDamaged
        {
            get { return _onDamaged; }
            set { SetProperty(ref _onDamaged, value); }
        }

        public string OnDeath
        {
            get { return _onDeath; }
            set { SetProperty(ref _onDeath, value); }
        }

        public string OnHeartbeat
        {
            get { return _onHeartbeat; }
            set { SetProperty(ref _onHeartbeat, value); }
        }

        public string OnDisturbed
        {
            get { return _onDisturbed; }
            set { SetProperty(ref _onDisturbed, value); }
        }

        public string OnLocked
        {
            get { return _onLocked; }
            set { SetProperty(ref _onLocked, value); }
        }

        public string OnUnlocked
        {
            get { return _onUnlocked; }
            set { SetProperty(ref _onUnlocked, value); }
        }

        public string OnAttacked
        {
            get { return _onAttacked; }
            set { SetProperty(ref _onAttacked, value); }
        }

        public string OnUsed
        {
            get { return _onUsed; }
            set { SetProperty(ref _onUsed, value); }
        }

        public LocalVariableDataObservable LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
        }
        
        public PlaceableDataObservable()
        {
            
        }
        public PlaceableDataObservable(PlaceableDataObservableValidator validator,
            IObjectMapper objectMapper,
            LocalVariableDataObservable.Factory localVariableFactory,
            PlaceableData data = null)
            : base(objectMapper, validator, data)
        {
            if (data == null)
            {
                GlobalID = Guid.NewGuid().ToString();

                Name = string.Empty;
                Tag = string.Empty;
                Resref = string.Empty;
                Description = string.Empty;
                Comment = string.Empty;

                IsPlot = false;
                IsKeyRequired = false;
                IsLocked = false;
                IsStatic = false;
                IsUseable = false;
                AutoRemoveKey = false;

                OnAttacked = string.Empty;
                OnClosed = string.Empty;
                OnDamaged = string.Empty;
                OnDeath = string.Empty;
                OnDisturbed = string.Empty;
                OnHeartbeat = string.Empty;
                OnLocked = string.Empty;
                OnOpened = string.Empty;
                OnUnlocked = string.Empty;
                OnUsed = string.Empty;

                LocalVariables = localVariableFactory.Invoke();
            }
            
            LocalVariables.VariablesPropertyChanged += (sender, args) => OnPropertyChanged();
            LocalVariables.VariablesCollectionChanged += (sender, args) => OnPropertyChanged();
            LocalVariables.VariablesItemPropertyChanged += (sender, args) => OnPropertyChanged();
        }
    }
}
