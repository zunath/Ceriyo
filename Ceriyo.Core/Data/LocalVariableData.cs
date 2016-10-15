using System;
using System.Collections.Generic;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalVariableData: BaseValidatable
    {
        private IList<LocalDoubleData> _localDoubles;
        private IList<LocalStringData> _localStrings;
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

        public IList<LocalStringData> LocalStrings
        {
            get { return _localStrings; }
            set
            {
                _localStrings = value;
                OnPropertyChanged();
            }
        }

        public IList<LocalDoubleData> LocalDoubles
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
            LocalStrings = new List<LocalStringData>();
            LocalDoubles = new List<LocalDoubleData>();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new LocalVariableDataValidator());
    }
}
