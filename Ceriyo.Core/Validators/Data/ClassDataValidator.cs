﻿using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validators.Data
{
    public class ClassDataValidator: AbstractValidator<ClassData>
    {
        public ClassDataValidator()
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotNull()
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotNull()
                .NotEmpty()
                .Length(1, 32);

        }
    }
}
