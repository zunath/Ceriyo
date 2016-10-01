using Ceriyo.Core.Data;
using Ceriyo.Toolset.WPF.Events;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ApplicationRootView
{
    public class ApplicationRootViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ModuleData _moduleData;

        public ApplicationRootViewModel()
        {

        }

        public ApplicationRootViewModel(IEventAggregator eventAggregator,
            ModuleData moduleData)
        {
            _eventAggregator = eventAggregator;
            _moduleData = moduleData;
            ChangeWindowTitle();

            eventAggregator.GetEvent<ModulePropertiesChangedEvent>().Subscribe(ChangeWindowTitle);
            eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ChangeWindowTitle);
            eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ChangeWindowTitle);
        }

        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { SetProperty(ref _windowTitle, value); }
        }

        private void ChangeWindowTitle()
        {
            if (string.IsNullOrWhiteSpace(_moduleData.Name))
            {
                WindowTitle = "Ceriyo Toolset";
            }
            else
            {
                WindowTitle = "Ceriyo Toolset - " + _moduleData.Name;
            }
        }

    }
}
