using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ItemDataObservable: ValidatableBindableBase<ItemData>
    {
        public delegate ItemDataObservable Factory(ItemData data = null);

        private ObservableCollectionEx<string> _itemPropertyResrefs;
        private ObservableCollectionEx<ClassRequirementDataObservable> _classRequirements;
        private LocalVariableDataObservable _localVariables;
        private string _onUnequipped;
        private string _onEquipped;
        private string _onUnacquired;
        private string _onAcquired;
        private string _onActivated;
        private string _comment;
        private string _description;
        private bool _isPlot;
        private bool _isStolen;
        private bool _isUndroppable;
        private string _itemTypeResref;
        private string _name;
        private string _tag;
        private string _resref;
        private string _globalID;
        
        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
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

        public string ItemTypeResref
        {
            get { return _itemTypeResref; }
            set { SetProperty(ref _itemTypeResref, value); }
        }

        public bool IsUndroppable
        {
            get { return _isUndroppable; }
            set { SetProperty(ref _isUndroppable, value); }
        }

        public bool IsStolen
        {
            get { return _isStolen; }
            set { SetProperty(ref _isStolen, value); }
        }

        public bool IsPlot
        {
            get { return _isPlot; }
            set { SetProperty(ref _isPlot, value); }
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

        public string OnActivated
        {
            get { return _onActivated; }
            set { SetProperty(ref _onActivated, value); }
        }

        public string OnAcquired
        {
            get { return _onAcquired; }
            set { SetProperty(ref _onAcquired, value); }
        }

        public string OnUnacquired
        {
            get { return _onUnacquired; }
            set { SetProperty(ref _onUnacquired, value); }
        }

        public string OnEquipped
        {
            get { return _onEquipped; }
            set { SetProperty(ref _onEquipped, value); }
        }

        public string OnUnequipped
        {
            get { return _onUnequipped; }
            set { SetProperty(ref _onUnequipped, value); }
        }

        public LocalVariableDataObservable LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
        }

        public ObservableCollectionEx<ClassRequirementDataObservable> ClassRequirements
        {
            get { return _classRequirements; }
            set { SetProperty(ref _classRequirements, value); }
        }

        public ObservableCollectionEx<string> ItemPropertyResrefs
        {
            get { return _itemPropertyResrefs; }
            set { SetProperty(ref _itemPropertyResrefs, value); }
        }
        
        public ItemDataObservable(ItemDataObservableValidator validator,
            IObjectMapper objectMapper,
            LocalVariableDataObservable.Factory localVariableFactory,
            ItemData data = null)
            :base(objectMapper, validator, data)
        {
            GlobalID = Guid.NewGuid().ToString();
            _localVariables = localVariableFactory.Invoke();
            _classRequirements = new ObservableCollectionEx<ClassRequirementDataObservable>();
            _itemPropertyResrefs = new ObservableCollectionEx<string>();
        }
    }
}
