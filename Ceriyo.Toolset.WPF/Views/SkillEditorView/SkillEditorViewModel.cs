using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Events.Skill;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.SkillEditorView
{
    public class SkillEditorViewModel : ValidatableBindableBase<SkillEditorViewModel>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly SkillDataObservable.Factory _skillFactory;

        public SkillEditorViewModel(
            IObjectMapper objectMapper,
            IEventAggregator eventAggregator,
            IDataService dataService,
            IPathService pathService,
            SkillEditorViewModelValidator validator,
            SkillDataObservable.Factory skillFactory)
            :base(objectMapper, validator)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _pathService = pathService;
            _skillFactory = skillFactory;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Skills = new ObservableCollectionEx<SkillDataObservable>();
            Scripts = new Dictionary<string, ScriptDataObservable>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            
            Skills.ItemPropertyChanged += SkillsOnItemPropertyChanged;

            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
        }

        private void ModuleLoaded(string moduleFileName)
        {
            LoadExistingData();
        }

        private void ModuleClosed()
        {
            Skills.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadExistingData();
        }

        private void LoadExistingData()
        {
            Skills.Clear();
            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Skill/", "*.dat");

            foreach (var file in files)
            {
                SkillData loaded = _dataService.Load<SkillData>(file);
                SkillDataObservable skill = _skillFactory.Invoke(loaded);
                Skills.Add(skill);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            _eventAggregator.GetEvent<SkillEditorValidityChangedEvent>().Publish(!HasErrors);
        }

        private void SkillsOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            SkillDataObservable skill = (SkillDataObservable) sender;
            _eventAggregator.GetEvent<SkillChangedEvent>().Publish(skill);
            RaiseValidityChangedEvent();
        }

        private ObservableCollectionEx<SkillDataObservable> _skills;

        public ObservableCollectionEx<SkillDataObservable> Skills
        {
            get { return _skills; }
            set { SetProperty(ref _skills, value); }
        }

        private SkillDataObservable _selectedSkill;
        public SkillDataObservable SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                SetProperty(ref _selectedSkill, value);
                OnPropertyChanged(nameof(IsSkillSelected));
            }
        }

        private Dictionary<string, ScriptDataObservable> _scripts;

        public Dictionary<string, ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsSkillSelected => SelectedSkill != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            var skill = _skillFactory.Invoke();
            skill.Name = "Skill" + (Skills.Count + 1);
            Skills.Add(skill);
            _eventAggregator.GetEvent<SkillCreatedEvent>().Publish(skill);
            RaiseValidityChangedEvent();
        }

        private void Delete()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Skill?",
                    Content = "Are you sure you want to delete this skill?"
                }, c =>
                {
                    if (!c.Confirmed) return;
                    _eventAggregator.GetEvent<SkillDeletedEvent>().Publish(SelectedSkill);
                    Skills.Remove(SelectedSkill);
                    RaiseValidityChangedEvent();
                });
        }
        
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }
    }
}
