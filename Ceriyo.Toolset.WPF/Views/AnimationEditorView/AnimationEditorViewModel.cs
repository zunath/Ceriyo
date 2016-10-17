using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;

namespace Ceriyo.Toolset.WPF.Views.AnimationEditorView
{
    public class AnimationEditorViewModel : ValidatableBindableBase<AnimationEditorViewModel>
    {
        public AnimationEditorViewModel(IObjectMapper objectMapper, AnimationEditorViewModelValidator validator)
            : base(objectMapper, validator)
        {

        }
    }
}
