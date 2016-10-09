using System;
using FluentValidation;

namespace Ceriyo.Core.Contracts
{
    public interface IValidatorFactory
    {
        IValidator<T> GetValidator<T>();
        IValidator GetValidator(Type type);
    }
}
