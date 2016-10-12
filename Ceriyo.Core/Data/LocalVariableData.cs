using System;
using System.Collections.Generic;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalVariableData: BaseValidatable
    {
        private Dictionary<string, float> _localFloats;
        private Dictionary<string, string> _localStrings;
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

        public Dictionary<string, string> LocalStrings
        {
            get { return _localStrings; }
            set
            {
                _localStrings = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<string, float> LocalFloats
        {
            get { return _localFloats; }
            set
            {
                _localFloats = value;
                OnPropertyChanged();
            }
        }

        public LocalVariableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalStrings = new Dictionary<string, string>();
            LocalFloats = new Dictionary<string, float>();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new LocalVariableDataValidator());
    }
}
