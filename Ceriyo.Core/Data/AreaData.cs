using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class AreaData: BaseValidatable
    {
        private string _globalID;
        private string _name;
        private string _tag;
        private string _resref;
        private string _comment;
        private string _description;
        private string _onAreaEnter;
        private string _onAreaExit;
        private string _onAreaHeartbeat;
        
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

        public string OnAreaEnter
        {
            get { return _onAreaEnter; }
            set
            {
                _onAreaEnter = value;
                OnPropertyChanged();
            }
        }

        public string OnAreaExit
        {
            get { return _onAreaExit; }
            set
            {
                _onAreaExit = value;
                OnPropertyChanged();
            }
        }

        public string OnAreaHeartbeat
        {
            get { return _onAreaHeartbeat; }
            set
            {
                _onAreaHeartbeat = value;
                OnPropertyChanged();
            }
        }

        public AreaData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new AreaDataValidator());
    }
}
