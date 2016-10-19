using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class SkillDataObservable: ValidatableBindableBase<SkillData>
    {
        public delegate SkillDataObservable Factory(SkillData data = null);


        private string _globalID;
        private string _name;
        private string _tag;
        private string _resref;
        private string _description;
        private string _comment;
        private bool _isPassive;
        private string _onActivated;

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

        public bool IsPassive
        {
            get { return _isPassive; }
            set { SetProperty(ref _isPassive, value); }
        }

        public string OnActivated
        {
            get { return _onActivated; }
            set { SetProperty(ref _onActivated, value); }
        }

        public SkillDataObservable()
        {
            
        }
        public SkillDataObservable(SkillDataObservableValidator validator,
            IObjectMapper objectMapper,
            SkillData data)
            :base(objectMapper, validator, data)
        {
            if (data != null) return;
            GlobalID = Guid.NewGuid().ToString();
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comment = string.Empty;
            IsPassive = false;
            OnActivated = string.Empty;
        }
    }
}
