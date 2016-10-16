using System;
using System.ComponentModel;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalVariableData: BaseValidatable
    {
        private BindingList<LocalDoubleData> _localDoubles;
        private BindingList<LocalStringData> _localStrings;
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

        public BindingList<LocalStringData> LocalStrings
        {
            get { return _localStrings; }
            set
            {
                _localStrings = value;
                OnPropertyChanged();
            }
        }

        public BindingList<LocalDoubleData> LocalDoubles
        {
            get { return _localDoubles; }
            set
            {
                _localDoubles = value;
                OnPropertyChanged();
            }
        }

        public LocalVariableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalStrings = new BindingList<LocalStringData>();
            LocalDoubles = new BindingList<LocalDoubleData>();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new LocalVariableDataValidator());
    }
}
