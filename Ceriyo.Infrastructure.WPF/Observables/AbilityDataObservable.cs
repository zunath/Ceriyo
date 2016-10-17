using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class AbilityDataObservable: ValidatableBindableBase<AbilityData>
    {
        public delegate AbilityDataObservable Factory(AbilityData ability = null);

        private string _globalID;

        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        private string _resref;
        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private bool _isPassive;

        public bool IsPassive
        {
            get { return _isPassive; }
            set { SetProperty(ref _isPassive, value); }
        }

        private string _onActivated;

        public string OnActivated
        {
            get { return _onActivated; }
            set { SetProperty(ref _onActivated, value); }
        }
        
        public AbilityDataObservable(AbilityDataObservableValidator validator, 
            IObjectMapper objectMapper,
            AbilityData ability = null) 
            : base(objectMapper, validator, ability)
        {
            GlobalID = Guid.NewGuid().ToString();
        }

    }
}
