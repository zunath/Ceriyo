using System;
using Autofac;
using FluentValidation;
using IValidatorFactory = Ceriyo.Core.Contracts.IValidatorFactory;

namespace Ceriyo.Infrastructure.Factory
{
    public class ValidatorFactory: ValidatorFactoryBase, IValidatorFactory
    {
        private readonly IComponentContext _context;
        public ValidatorFactory(IComponentContext context)
        {
            _context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            var validator = _context.ResolveOptionalKeyed<IValidator>(validatorType);
            return validator;
        }
    }
}
