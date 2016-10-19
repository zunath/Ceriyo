using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;

namespace Ceriyo.Toolset.WPF.Views.TilesetEditorView
{
    public class TilesetEditorViewModel : ValidatableBindableBase<TilesetEditorViewModel>
    {
        public TilesetEditorViewModel(IObjectMapper objectMapper,
            TilesetEditorViewModelValidator validator)
            :base(objectMapper, validator)
        {

        }
    }
}
