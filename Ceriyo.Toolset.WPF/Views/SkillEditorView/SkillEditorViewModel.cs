using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.SkillEditorView
{
    public class SkillEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public SkillEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Skills = new ObservableCollectionEx<SkillData>();
            Scripts = new Dictionary<string, ScriptData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();

            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);

            Skills.ItemPropertyChanged += SkillsOnItemPropertyChanged;
        }

        private void SkillsOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            SkillData skillChanged = sender as SkillData;
            _eventAggregator.GetEvent<SkillChangedEvent>().Publish(skillChanged);
        }

        private ObservableCollectionEx<SkillData> _skills;

        public ObservableCollectionEx<SkillData> Skills
        {
            get { return _skills; }
            set { SetProperty(ref _skills, value); }
        }

        private SkillData _selectedSkill;
        public SkillData SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                SetProperty(ref _selectedSkill, value);
                OnPropertyChanged("IsSkillSelected");
            }
        }

        private Dictionary<string, ScriptData> _scripts;

        public Dictionary<string, ScriptData> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsSkillSelected => SelectedSkill != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            SkillData skill = new SkillData
            {
                Name = "Skill" + (Skills.Count + 1)
            };
            Skills.Add(skill);

            _eventAggregator.GetEvent<SkillCreatedEvent>().Publish(skill);
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
                    string globalID = SelectedSkill.GlobalID;
                    Skills.Remove(SelectedSkill);
                    _eventAggregator.GetEvent<SkillDeletedEvent>().Publish(globalID);
                });
        }

        private void DataEditorClosed(bool saveData)
        {
            if (saveData)
            {

            }
        }

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
