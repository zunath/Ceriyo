using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class TilesetDataObservable: ValidatableBindableBase<TilesetData>
    {
        public delegate TilesetDataObservable Factory();

        private string _globalID;
        private string _name;
        private string _tag;
        private string _resref;
        private string _description;
        private string _comment;

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

        public TilesetDataObservable(TilesetDataObservableValidator validator,
            IObjectMapper objectMapper)
            : base(objectMapper, validator)
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
