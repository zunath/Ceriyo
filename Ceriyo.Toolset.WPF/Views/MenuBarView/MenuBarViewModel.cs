using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.MenuBarView
{
    public class MenuBarViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _loadedModuleFileName;

        public MenuBarViewModel()
        {
            
        }

        public MenuBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NewModuleCommand = new DelegateCommand(NewModule);
            OpenModuleCommand = new DelegateCommand(OpenModule);
            CloseModuleCommand = new DelegateCommand(CloseModule);
            SaveCommand = new DelegateCommand(Save);
            SaveAsCommand = new DelegateCommand(SaveAs);
            ImportCommand = new DelegateCommand(Import);
            ExportCommand = new DelegateCommand(Export);
            ExitCommand = new DelegateCommand(Exit);
            UndoCommand = new DelegateCommand(Undo);
            RedoCommand = new DelegateCommand(Redo);
            CopyCommand = new DelegateCommand(Copy);
            CutCommand = new DelegateCommand(Cut);
            PasteCommand = new DelegateCommand(Paste);
            ModulePropertiesCommand = new DelegateCommand(ModuleProperties);
            DataEditorCommand = new DelegateCommand(DataEditor);
            ResourceEditorCommand = new DelegateCommand(ResourceEditor);
            BuildModuleCommand = new DelegateCommand(BuildModule);
            ManageResourcesCommand = new DelegateCommand(ManageResources);
            AboutCommand = new DelegateCommand(About);

            NewModuleRequest = new InteractionRequest<INotification>();
            SaveModuleAsRequest = new InteractionRequest<INotification>();
            OpenModuleRequest = new InteractionRequest<INotification>();
            OpenModulePropertiesRequest = new InteractionRequest<INotification>();
            OpenDataEditorRequest = new InteractionRequest<INotification>();
            OpenManageResourcesRequest = new InteractionRequest<INotification>();
            OpenResourceEditorRequest = new InteractionRequest<INotification>();
            OpenBuildModuleRequest = new InteractionRequest<INotification>();
            OpenAboutRequest = new InteractionRequest<INotification>();
            
            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(() => IsModuleLoaded = false);
        }

        private void ModuleLoaded(string fileName)
        {
            IsModuleLoaded = true;
            _loadedModuleFileName = fileName;
        }

        public DelegateCommand NewModuleCommand { get; set; }
        public InteractionRequest<INotification> NewModuleRequest { get; }
        private void NewModule()
        {
            NewModuleRequest.Raise(new Notification
            {
                Content = "New Module",
                Title = "New Module"
            });
        }

        public DelegateCommand OpenModuleCommand { get; set; }
        public InteractionRequest<INotification> OpenModuleRequest { get; }

        private void OpenModule()
        {
            OpenModuleRequest.Raise(new Notification
            {
                Content = "Open Module",
                Title = "Open Module"
            });
        }

        public DelegateCommand CloseModuleCommand { get; set; }

        private void CloseModule()
        {
            _eventAggregator.GetEvent<ModuleClosedEvent>().Publish();
        }

        public DelegateCommand SaveCommand { get; set; }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(_loadedModuleFileName))
            {
                SaveAs();
                return;
            }
            _eventAggregator.GetEvent<ModuleSavedEvent>().Publish(_loadedModuleFileName);
        }

        public DelegateCommand SaveAsCommand { get; set; }
        public InteractionRequest<INotification> SaveModuleAsRequest { get; }

        private void SaveAs()
        {
            SaveModuleAsRequest.Raise(new Notification
            {
                Title = "Save Module",
                Content = "Save Module"
            }, (notification) =>
            {
                _loadedModuleFileName = notification.Content as string;
            });
        }
        
        public DelegateCommand ImportCommand { get; set; }

        private void Import()
        {
            
        }

        public DelegateCommand ExportCommand { get; set; }

        private void Export()
        {
            
        }

        public DelegateCommand ExitCommand { get; set; }

        private void Exit()
        {
            _eventAggregator.GetEvent<ModuleClosedEvent>().Publish();
            _eventAggregator.GetEvent<ApplicationClosedEvent>().Publish();
        }

        public DelegateCommand UndoCommand { get; set; }

        private void Undo()
        {
            
        }

        public DelegateCommand RedoCommand { get; set; }

        private void Redo()
        {
            
        }

        public DelegateCommand CopyCommand { get; set; }

        private void Copy()
        {
            
        }

        public DelegateCommand CutCommand { get; set; }

        private void Cut()
        {
            
        }

        public DelegateCommand PasteCommand { get; set; }

        private void Paste()
        {
            
        }

        public DelegateCommand ModulePropertiesCommand { get; set; }
        public InteractionRequest<INotification> OpenModulePropertiesRequest { get; }

        private void ModuleProperties()
        {
            OpenModulePropertiesRequest.Raise(new Notification
            {
                Title = "Module Properties",
                Content = "Module Properties"
            });
        }

        public DelegateCommand DataEditorCommand { get; set; }
        public InteractionRequest<INotification> OpenDataEditorRequest { get; }
        private void DataEditor()
        {
            OpenDataEditorRequest.Raise(new Notification
            {
                Title = "Data Editor",
                Content = "Data Editor"
            });
        }

        public DelegateCommand ResourceEditorCommand { get; set; }
        public InteractionRequest<INotification> OpenResourceEditorRequest { get; }
        private void ResourceEditor()
        {
            OpenResourceEditorRequest.Raise(new Notification
            {
                Title = "Resource Editor",
                Content = "Resource Editor"
            });
        }

        public DelegateCommand ManageResourcesCommand { get; set; }
        public InteractionRequest<INotification> OpenManageResourcesRequest { get; }

        private void ManageResources()
        {
            OpenManageResourcesRequest.Raise(new Notification
            {
                Title = "Manage Resources",
                Content = "Manage Resources"
            });
        }

        public DelegateCommand BuildModuleCommand { get; set; }
        public InteractionRequest<INotification> OpenBuildModuleRequest { get; }
        private void BuildModule()
        {
            OpenBuildModuleRequest.Raise(new Notification
            {
                Title = "Build Module",
                Content = "Build Module"
            });
        }

        public DelegateCommand AboutCommand { get; set; }
        public InteractionRequest<INotification> OpenAboutRequest { get; }

        private void About()
        {
            OpenAboutRequest.Raise(new Notification
            {
                Title = "About",
                Content = "About"
            });
        }

        private bool _isModuleLoaded;

        public bool IsModuleLoaded
        {
            get { return _isModuleLoaded; }
            set { SetProperty(ref _isModuleLoaded, value); }
        }
        

        

    }
}
