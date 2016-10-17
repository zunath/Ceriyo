using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class AreaDataObservable: ValidatableBindableBase<AreaData>
    {
        public delegate AreaDataObservable Factory();

        private string _globalID;
        private string _name;
        private string _tag;
        private string _resref;
        private string _comment;
        private string _description;
        private string _onAreaEnter;
        private string _onAreaExit;
        private string _onAreaHeartbeat;
        

        public string OnAreaHeartbeat
        {
            get { return _onAreaHeartbeat; }
            set { SetProperty(ref _onAreaHeartbeat, value); }
        }

        public string OnAreaExit
        {
            get { return _onAreaExit; }
            set { SetProperty(ref _onAreaExit, value); }
        }

        public string OnAreaEnter
        {
            get { return _onAreaEnter; }
            set { SetProperty(ref _onAreaEnter, value); }
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

        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }
        
        public AreaDataObservable(AreaDataObservableValidator validator,
            IObjectMapper objectMapper)
            :base(objectMapper, validator)
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
