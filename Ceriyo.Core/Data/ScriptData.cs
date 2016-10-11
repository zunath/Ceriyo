using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class ScriptData: BaseValidatable
    {
        private string _resref;
        private string _name;
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
        
        public ScriptData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new ScriptDataValidator());
    }
}
