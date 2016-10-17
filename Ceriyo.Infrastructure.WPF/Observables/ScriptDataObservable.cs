using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ScriptDataObservable: ValidatableBindableBase<ScriptData>
    {
        public delegate ScriptDataObservable Factory();

        private string _globalID;
        private string _name;
        private string _resref;

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

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }
        
        public ScriptDataObservable(ScriptDataObservableValidator validator,
            IObjectMapper objectMapper)
            :base(objectMapper, validator)
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
