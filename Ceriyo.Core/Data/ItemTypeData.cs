using System;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class ItemTypeData: BaseValidatable
    {
        public string GlobalID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }

        public ItemTypeData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new ItemTypeDataValidator());
    }
}
