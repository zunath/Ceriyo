using Prism.Mvvm;

namespace Ceriyo.Server.WPF.Views.DescriptionView
{
    public class DescriptionViewModel : BindableBase
    {
        public DescriptionViewModel()
        {

        }

        private string _serverDescription;

        public string ServerDescription
        {
            get { return _serverDescription; }
            set { SetProperty(ref _serverDescription, value); }
        }

        private string _serverAnnouncement;

        public string ServerAnnouncement
        {
            get { return _serverAnnouncement; }
            set { SetProperty(ref _serverAnnouncement, value); }
        }

    }
}
