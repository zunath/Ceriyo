using Ceriyo.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.DescriptionView
{
    public class DescriptionViewModel : BindableBase
    {
        private ServerSettings _settings;

        public DescriptionViewModel()
        {
        }

        public DescriptionViewModel(ServerSettings settings)
        {
            _settings = settings;
        }

        public ServerSettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }
    }
}
