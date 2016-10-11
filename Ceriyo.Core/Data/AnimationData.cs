using System;
using System.ComponentModel;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class AnimationData: BaseValidatable
    {
        private BindingList<FrameData> _frames;
        private string _comment;
        private string _description;
        private string _resref;
        private string _tag;
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

        public BindingList<FrameData> Frames
        {
            get { return _frames; }
            set
            {
                if (Equals(value, _frames)) return;
                _frames = value;
                OnPropertyChanged();
            }
        }


        public AnimationData()
        {
            GlobalID = Guid.NewGuid().ToString();
            Frames = new BindingList<FrameData>();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new AnimationDataValidator());
    }
}
