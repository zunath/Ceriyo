using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ClassDataObservable: ValidatableBindableBase<ClassData>
    {
        public delegate ClassDataObservable Factory(ClassData data = null);

        private string _globalID;
        private string _name;
        private string _tag;
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

        public ClassDataObservable()
        {
            
        }
        public ClassDataObservable(ClassDataObservableValidator validator,
            IObjectMapper objectMapper,
            ClassData data)
            :base(objectMapper, validator, data)
        {
            if (data != null) return;
            GlobalID = Guid.NewGuid().ToString();
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
        }
    }
}
