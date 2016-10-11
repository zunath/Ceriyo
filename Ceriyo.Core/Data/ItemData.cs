using System;
using System.ComponentModel;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class ItemData: BaseValidatable
    {
        private BindingList<string> _itemPropertyResrefs;
        private BindingList<ClassRequirementData> _classRequirements;
        private LocalVariableData _localVariables;
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
            set
            {
                if (value == _globalID) return;
                _globalID = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Tag
        {
            get { return _tag; }
            set
            {
                if (value == _tag) return;
                _tag = value;
                OnPropertyChanged();
            }
        }

        public string Resref
        {
            get { return _resref; }
            set
            {
                if (value == _resref) return;
                _resref = value;
                OnPropertyChanged();
            }
        }

        public string ItemTypeResref
        {
            get { return _itemTypeResref; }
            set
            {
                if (value == _itemTypeResref) return;
                _itemTypeResref = value;
                OnPropertyChanged();
            }
        }

        public bool IsUndroppable
        {
            get { return _isUndroppable; }
            set
            {
                if (value == _isUndroppable) return;
                _isUndroppable = value;
                OnPropertyChanged();
            }
        }

        public bool IsStolen
        {
            get { return _isStolen; }
            set
            {
                if (value == _isStolen) return;
                _isStolen = value;
                OnPropertyChanged();
            }
        }

        public bool IsPlot
        {
            get { return _isPlot; }
            set
            {
                if (value == _isPlot) return;
                _isPlot = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value == _comment) return;
                _comment = value;
                OnPropertyChanged();
            }
        }


        public string OnActivated
        {
            get { return _onActivated; }
            set
            {
                if (value == _onActivated) return;
                _onActivated = value;
                OnPropertyChanged();
            }
        }

        public string OnAcquired
        {
            get { return _onAcquired; }
            set
            {
                if (value == _onAcquired) return;
                _onAcquired = value;
                OnPropertyChanged();
            }
        }

        public string OnUnacquired
        {
            get { return _onUnacquired; }
            set
            {
                if (value == _onUnacquired) return;
                _onUnacquired = value;
                OnPropertyChanged();
            }
        }

        public string OnEquipped
        {
            get { return _onEquipped; }
            set
            {
                if (value == _onEquipped) return;
                _onEquipped = value;
                OnPropertyChanged();
            }
        }

        public string OnUnequipped
        {
            get { return _onUnequipped; }
            set
            {
                if (value == _onUnequipped) return;
                _onUnequipped = value;
                OnPropertyChanged();
            }
        }

        public LocalVariableData LocalVariables
        {
            get { return _localVariables; }
            set
            {
                if (Equals(value, _localVariables)) return;
                _localVariables = value;
                OnPropertyChanged();
            }
        }

        public BindingList<ClassRequirementData> ClassRequirements
        {
            get { return _classRequirements; }
            set
            {
                if (Equals(value, _classRequirements)) return;
                _classRequirements = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ItemPropertyResrefs
        {
            get { return _itemPropertyResrefs; }
            set
            {
                if (Equals(value, _itemPropertyResrefs)) return;
                _itemPropertyResrefs = value;
                OnPropertyChanged();
            }
        }

        public ItemData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
            ClassRequirements = new BindingList<ClassRequirementData>();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new ItemDataValidator());

    }
}
