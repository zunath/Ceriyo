using Ceriyo.Core.Data;
using Ceriyo.Core.Validation.Components;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class ModuleDataValidator: AbstractValidator<ModuleData>
    {
        public ModuleDataValidator()
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

            RuleFor(x => x.Comment)
                .Length(0, 5000);

            RuleFor(x => x.Description)
                .Length(0, 2000);

            RuleForEach(x => x.AbilityIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.ClassIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.CreatureIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.ItemIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.ItemPropertyIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.PlaceableIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.ScriptIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.SkillIDs)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.TilesetIDs)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.LocalVariables)
                .Must(x => !x.HasErrors)
                .WithMessage("Local variables invalid.");

            RuleForEach(x => x.LevelChart)
                .SetValidator(new ClassLevelValidator());

            RuleForEach(x => x.ResourcePacks)
                .NotNull()
                .Length(1, 64);
        }
    }
}
