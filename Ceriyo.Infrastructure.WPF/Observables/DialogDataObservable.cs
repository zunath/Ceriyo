﻿using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class DialogDataObservable: ValidatableBindableBase<DialogData>
    {
        public delegate DialogDataObservable Factory();

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
        

        public DialogDataObservable(DialogDataObservableValidator validator,
            IObjectMapper objectMapper)
            :base(objectMapper, validator)
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
