using Ceriyo.Infrastructure.WPF.BindableBases;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.AnimationEditorView
{
    public class AnimationEditorViewModel : ValidatableBindableBase
    {
        public AnimationEditorViewModel()
        {

        }

        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new AnimationEditorViewModelValidator());
    }
}
