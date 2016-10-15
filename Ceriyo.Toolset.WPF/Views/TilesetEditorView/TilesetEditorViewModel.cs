using Ceriyo.Infrastructure.WPF.BindableBases;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.TilesetEditorView
{
    public class TilesetEditorViewModel : ValidatableBindableBase
    {
        public TilesetEditorViewModel()
        {

        }

        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new TilesetEditorViewModelValidator());
    }
}
